using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerHealthController : MonoBehaviour, IDamage
{
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentHealth<=0)
        {
            Die();
        }
        playerAnimator.SetInteger("Health", currentHealth);
        
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1.0f, DamageType.Physical);
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIHealthController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        currentHealth -= (int)damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        UIHealthController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    private void Die()
    {
        playerMovement.enabled = false;
    }
}
