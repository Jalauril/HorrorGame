using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public int weaponNumber;
    public int charges;

    Ammo ammo = new Ammo();
	
	void Awake ()
    {
        ammo.charges = charges;
        ammo.weaponNumber = weaponNumber;
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("PickUpAmmo", ammo, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject, 0f);
        }
    }
}
