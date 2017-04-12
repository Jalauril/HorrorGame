using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject[] enemies;
    public float delay = 1f;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("ActivateEnemies", delay);
        }
    }

    void ActivateEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().active = true;
        }
    }
}
