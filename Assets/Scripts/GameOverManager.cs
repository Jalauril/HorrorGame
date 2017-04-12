using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public PlayerHealth playerHealth;

    bool gameOver;

    Animator anim;

	void Awake ()
    {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	void Update ()
    {
		if (playerHealth.currentHealth <= 0 && gameOver == false)
        {
            anim.SetTrigger("GameOver");
            gameOver = true;
        }

        if (gameOver && Input.GetButtonDown("Jump"))
        {
            Restart();
        }
	}

    public void Restart ()
    {
        SceneManager.LoadScene("Level001");
    }
}
