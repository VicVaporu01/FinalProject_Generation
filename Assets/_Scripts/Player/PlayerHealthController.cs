using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerHealthController : MonoBehaviour, IDamage
{
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private int playerCurrentHealth, playerMaxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    void Start()
    {
        /*
         * This is to get the health stored in the game manager and set it to the
         * player to keep the information between scenes
         */
        playerMaxHealth = GameManager.Instance.GetMaxHealth();
        playerCurrentHealth = GameManager.Instance.GetCurrentHealth();
        
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCurrentHealth = playerMaxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerAnimator.SetInteger("Health", playerCurrentHealth);
        
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    // public void HealPlayer()
    // {
    //     playerCurrentHealth++;
    //     if (playerCurrentHealth > playerMaxHealth)
    //     {
    //         playerCurrentHealth = playerMaxHealth;
    //     }
    //
    //     UIHealthController.instance.UpdateHealthDisplay(playerCurrentHealth, playerMaxHealth);
    // }

    public void TakeDamage(float damage, DamageType damageType)
    {
        CinemachineMovimientoCamara.Instance.MoverCamara(5, 5, 0.5f);
        playerCurrentHealth = GameManager.Instance.TakeDamage(damage, damageType);
        if (playerMaxHealth<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerMovement.enabled = false;
    }
}
