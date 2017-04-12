using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int damage = 10;
    [HideInInspector]public bool playerInRange;
    public float windUp;
    public float windDown;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth health;
    float timer = 0f;
    Animator anim;
    bool attacking;
    GunHit hit = new GunHit();

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        hit.damage = damage;
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (health.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (timer <= 0 && playerInRange && attacking == false)
            {
                anim.SetTrigger("Attack");
                timer = windUp;
                attacking = true;
            }

            if (timer <= 0f && attacking == true)
            {
                if (playerInRange)
                    playerHealth.SendMessage("TakeHit", hit, SendMessageOptions.DontRequireReceiver);
                timer = windDown;
                attacking = false;
            }
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("PlayerDead", true);
        }
	}
}
