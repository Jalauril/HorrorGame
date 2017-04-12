using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickUp : MonoBehaviour {


    int health = 40;
    Level001Manager manager;
    GameObject player;
    AudioSource explosionAudio;
    GunHit explosion = new GunHit();

    void Awake ()
    {
        manager = GameObject.Find("GameManager").GetComponent<Level001Manager>();
        player = GameObject.FindGameObjectWithTag("Player");
        explosionAudio = GetComponent<AudioSource>();
        explosion.damage = 1000;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            manager.objectivePickedUp = true;
            Destroy(gameObject, 0f);
        }
    }

    void TakeHit(GunHit hit)
    {
        health -= hit.damage;

        if (health <= 0)
        {
            explosionAudio.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
            player.SendMessage("TakeHit", explosion, SendMessageOptions.DontRequireReceiver);
        }
    }
}
