using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public int health = 60;
    public float effectRadius = 10f;
    public float maximumDamage = 150f;

    ParticleSystem explosionParticles;
    AudioSource explosionAudio;
    Light explosionLight;
    WaitForSeconds explosionDuration = new WaitForSeconds(0.1f);


    void Awake ()
    {
        explosionAudio = GetComponent<AudioSource>();
        explosionParticles = GetComponentInChildren<ParticleSystem>();
        explosionLight = GetComponent<Light>();
	}

    void TakeHit(GunHit hit)
    {
        health -= hit.damage;

        if (health <= 0)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        int layerMask = 1 << 8;
        explosionAudio.Play();
        explosionParticles.Play();

        explosionLight.enabled = true;
        yield return explosionDuration;
        explosionLight.enabled = false;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Collider[] colls = Physics.OverlapSphere(transform.position, effectRadius, layerMask);

        foreach (Collider col in colls)
        {
            float distance = Vector3.Distance(col.transform.position, transform.position);
            float fdamage = maximumDamage * (1f - (distance / effectRadius));
            if (fdamage < 0f)
                fdamage = 0f;

            GunHit blastHit = new GunHit();
            blastHit.damage = Mathf.RoundToInt(fdamage);

            col.gameObject.SendMessage("TakeHit", blastHit, SendMessageOptions.DontRequireReceiver);
        }

        
        Destroy(gameObject, 2f);
    }
}
