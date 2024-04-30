using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerHealthController : MonoBehaviour, IDamage
{
    [Header("References")]
    [SerializeField] private PlayerStats playerStats;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    [Header("Health Values")]
    private int playerCurrentHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public event EventHandler PlayerDead;

    [SerializeField] private int healTestValue;

    void Start()
    {
        playerCurrentHealth = GameManager.Instance.GetCurrentHealth();

        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        theSR = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerStats.OnStatsChanged += ChangePlayerMaxHealth;
    }

    private void OnDisable()
    {
        playerStats.OnStatsChanged -= ChangePlayerMaxHealth;
    }

    private void ChangePlayerMaxHealth(int newSpeed, int newDamage, int newMaxLife, int newMagicDamage, int newBulletAmountStats)
    {
        playerCurrentHealth = GameManager.Instance.ChangePlayerMaxHealth(newMaxLife);
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

    public void HealPlayer(int healtAmount)
    {
        playerCurrentHealth = GameManager.Instance.HealDamage(healtAmount);
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        CinemachineMovimientoCamara.Instance.MoverCamara(5, 5, 0.5f);

        playerCurrentHealth = GameManager.Instance.TakeDamage(damage, damageType);

        if (playerCurrentHealth <= 0)
        {
            Die();

            PlayerDead?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Die()
    {
        playerMovement.enabled = false;
    }
}
