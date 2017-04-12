using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGun : GunBase
{
    public int pelletCount = 10;
    public Light[] muzzleLights;
    public ParticleSystem muzzleParticles;
    public float spreadAngle = 0f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);
    
    protected override void PrimaryFire()
    {
        StartCoroutine(Shot());

        for (int i = 0; i < pelletCount; i++)
        {
            RaycastHit hit;
            Quaternion fireRotation = Quaternion.LookRotation(cam.transform.forward);
            fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0f, spreadAngle));

            if (Physics.Raycast(cam.transform.position, fireRotation * Vector3.forward, out hit, maxRange))
            {
                GunHit gunHit = new GunHit();
                gunHit.damage = damage;
                gunHit.raycasthit = hit;
                hit.collider.SendMessage("TakeHit", gunHit, SendMessageOptions.DontRequireReceiver);
                /*
                GameObject ball;
                ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                ball.transform.position = hit.point;*/
            }
        }
    }

    private IEnumerator Shot()
    {
        gunAudio.Play();
        muzzleParticles.Emit(20);

        for (int i = 0; i < muzzleLights.Length; i++)
        {
            muzzleLights[i].enabled = true;
            yield return shotDuration;
            muzzleLights[i].enabled = false;

        }
    }

}
