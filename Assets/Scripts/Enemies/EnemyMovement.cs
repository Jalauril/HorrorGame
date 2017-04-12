using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [HideInInspector]public bool hasNav = false;
    public bool active = true;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyAttack attack;
    NavMeshAgent nav;
    Rigidbody rigid;
    Animator anim;
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (nav != null)
        {
            hasNav = true;
        }
    }

    void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        nav.enabled = false;
        rigid.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            active = true;
            attack.playerInRange = true;
            nav.Stop();
            anim.SetBool("Walking", false);
        }
    }

    void OnTriggerExit(Collider other)
    { 
        if (other.gameObject == player)
        {
            attack.playerInRange = false;
            nav.enabled = false;
            nav.enabled = true;
            anim.SetBool("Walking", true);
        }
    }
	
	
	void Update ()
    {
        if (active)
        {
            if (rigid.velocity.magnitude < 0.005f && rigid.angularVelocity.magnitude < 0.005f && rigid.isKinematic == false)
            {
                rigid.velocity = Vector3.zero;
                rigid.isKinematic = true;
                nav.enabled = true;
                anim.SetBool("Walking", false);
            }

            if (nav.enabled == true)
            {
                if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && attack.playerInRange == false)
                {
                    SetDestination();
                    anim.SetBool("Walking", true);
                }
                else
                {
                    nav.Stop();
                    anim.SetBool("Walking", false);
                }
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
    }

    public void Knockback(float force, Vector3 direction)
    {
        nav.enabled = false;
        rigid.isKinematic = false;
        rigid.AddForce(direction * force, ForceMode.Impulse);
    }

    public void SetDestination()
    {
        nav.SetDestination(player.transform.position);
    }
}
