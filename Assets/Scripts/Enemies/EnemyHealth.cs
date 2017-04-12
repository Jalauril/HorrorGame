using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth = 100;
    [HideInInspector]public bool hasNav = false;
    public float sinkSpeed = 0.001f;
    public float waitBeforeSinking = 2f;

    Animator anim;
    NavMeshAgent nav;
    EnemyAttack attack;
    EnemyMovement movement;
    bool isDead;
    float timer = 0f;

	void Awake ()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        attack = GetComponent<EnemyAttack>();
        movement = GetComponent<EnemyMovement>();
        isDead = false;
        nav = GetComponent<NavMeshAgent>();
	}
	

	void Update ()
    {
        if (isDead)
        {
            timer += Time.deltaTime;
            if (timer >= waitBeforeSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }
	}


    public void Kill()
    {
        if (isDead)
            return;
        else
        {
            currentHealth = 0;

            movement.enabled = false;
            nav.enabled = false;
            attack.enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            anim.SetBool("IsDead", true);
            isDead = true;

            Destroy(gameObject, 5f);

        }
    }

    void TakeHit(GunHit gunHit)
    {
        if (isDead)
            return;

        movement.active = enabled;
        currentHealth -= gunHit.damage;
        anim.SetTrigger("Hit");

        if (currentHealth <= 0f && isDead == false)
        {
            movement.enabled = false;
            nav.enabled = false;
            attack.enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            anim.SetBool("IsDead", true);
            isDead = true;

            Destroy(gameObject, 5f);
        }
    }
}
