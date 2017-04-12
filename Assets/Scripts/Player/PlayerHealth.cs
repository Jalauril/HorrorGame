using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Animator anim;

    private PlayerMovement playerMovement;
    private WeaponManager weaponsManager;

	void Awake ()
    {
        currentHealth = startingHealth;
        playerMovement = GetComponent<PlayerMovement>();
        weaponsManager = GetComponent<WeaponManager>();
    }
	

	void Update ()
    {

	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Damaged");

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void TakeHit(GunHit hit)
    {
        currentHealth -= hit.damage;
        
        anim.SetTrigger("Damaged");

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        playerMovement.enabled = false;
        weaponsManager.Disarm();
    }

    void Heal(Heal heal)
    {
        currentHealth += heal.health;
        if (currentHealth > startingHealth)
            currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }
}
