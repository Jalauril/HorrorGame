using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public int weaponNumber;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("PickUpWeapon", weaponNumber, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject, 0f);
        }
    }
}
