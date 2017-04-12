using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour {
    public int healing = 100;

    Heal heal = new Heal();
	
	void Awake ()
    {
        heal.health = 100;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Heal", heal, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject, 0f);
        }
    }
}
