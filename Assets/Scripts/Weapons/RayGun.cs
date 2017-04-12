using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : GunBase
{
    public Light[] muzzleLights;

    private LineRenderer gunLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);

    void Awake()
    {
        gunLine = GetComponentInChildren<LineRenderer>();
    }

    protected override void PrimaryFire()
    {
        StartCoroutine(Shot());

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        Quaternion fireRotation = Quaternion.LookRotation(cam.transform.forward);

        gunLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fireRotation * Vector3.forward, out hit, maxRange))
        {
            gunLine.SetPosition(1, hit.point);

            GunHit gunHit = new GunHit();
            gunHit.damage = damage;
            gunHit.raycasthit = hit;
            hit.collider.SendMessage("TakeHit", gunHit, SendMessageOptions.DontRequireReceiver);
        }

        else
        {
            gunLine.SetPosition(1, rayOrigin + (cam.transform.forward * maxRange));
        }
    }

    private IEnumerator Shot()
    {
        gunAudio.Play();

        gunLine.enabled = true;
        for (int i = 0; i < muzzleLights.Length; i++)
        {
            muzzleLights[i].enabled = true;
        }

        yield return shotDuration;

        gunLine.enabled = false;
        for (int i = 0; i < muzzleLights.Length; i++)
        {
            muzzleLights[i].enabled = false;
        }
    }
}
