using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public int currentWeapon = 0;
    public bool shotgun;
    public Transform[] weapons;

    public int shotgunCharges;

    private bool disarmed = false;
    private Light flashlight;

	void Start ()
    {
        flashlight = GetComponentInChildren<Light>();
        ChangeWeapon(0);
	}
	
	void Update ()
    {
        if (!disarmed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeWeapon(0);

            if (Input.GetKeyDown(KeyCode.Alpha2) && shotgun)
                ChangeWeapon(1);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeWeapon(2);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                ChangeWeapon(3);

            if (Input.GetKeyDown(KeyCode.Alpha5))
                ChangeWeapon(4);

            if (Input.GetKeyDown(KeyCode.F))
                flashlight.enabled = !flashlight.enabled;
        }
    }

    public void ChangeWeapon(int num)
    {
        currentWeapon = num;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }

    public void Disarm()
    {
        foreach (Transform weapon in weapons)
            weapon.gameObject.SetActive(false);
        disarmed = true;
        flashlight.enabled = false;
    }

    void PickUpWeapon(int num)
    {
        if (num == 1)
            shotgun = true;
    }

    void PickUpAmmo(Ammo ammo)
    {
        weapons[ammo.weaponNumber].gameObject.GetComponent<GunBase>().AddCharges(ammo.charges);
    }
}
