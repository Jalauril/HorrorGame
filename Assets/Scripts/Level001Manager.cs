using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level001Manager : MonoBehaviour
{
    
    public GameObject enemy;
    public GameObject objective;
    public float spawnTime;
    public Transform[] enemySpawns;
    public Transform[] objectiveSpawns;
    [HideInInspector]public bool objectivePickedUp = false;
    public Animator endAnim;
    public Canvas briefing;
    
    PlayerHealth playerHealth;
    bool spawning;
    bool briefed;
    float timer = 0f;
    GameObject player;
    GameObject[] enemies;
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        SpawnObjective();
        spawning = false;
    }


    void Update()
    {
        timer -= Time.deltaTime;
        
        if (playerHealth.currentHealth > 0 && spawning && timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnTime + Random.Range(0f, 2f);

        }
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, enemySpawns.Length);
        
        Instantiate(enemy, enemySpawns[spawnPointIndex].position, enemySpawns[spawnPointIndex].rotation);
    }

    void SpawnObjective()
    {
        int spawnPointIndex = Random.Range(0, objectiveSpawns.Length);
        Instantiate(objective, objectiveSpawns[spawnPointIndex].position, objectiveSpawns[spawnPointIndex].rotation);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && objectivePickedUp)
        {
            endAnim.SetBool("MissionCompleted", true);
            DestroyEnemies();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            briefing.enabled = false;
            spawning = true;
        }
    }

    void DestroyEnemies()
    {
        spawning = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyHealth health = enemies[i].GetComponent<EnemyHealth>();
            health.Kill();
        }
    }
}