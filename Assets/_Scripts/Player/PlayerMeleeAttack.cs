using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Damage Control")]
    [SerializeField] private int attackDamage;
    [SerializeField] private DamageType damageType;
    [SerializeField] private int maxAttackDamage;
    [SerializeField] private int minAttackDamage;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] hitSounds;

    private void OnEnable()
    {
        playerStats.OnStatsChanged += ChangeAttackDamage;
    }

    private void OnDisable()
    {
        playerStats.OnStatsChanged -= ChangeAttackDamage;
    }

    private void Start()
    {
        attackDamage = (int)MathF.Round(playerStats.GetNewStatValue(GameManager.Instance.damangeStats, maxAttackDamage, minAttackDamage));
    }

    private void ChangeAttackDamage(int newSpeed, int newDamage, int newMaxLife, int newMagicDamage, int newBulletAmountStats)
    {
        attackDamage = (int)MathF.Round(playerStats.GetNewStatValue(newDamage, maxAttackDamage, minAttackDamage));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamage objectHit))
            {
                AudioManager.Instance.PlaySoundEffect(hitSounds[Random.Range(0, hitSounds.Length)]);

                objectHit.TakeDamage(attackDamage, damageType);
            }
        }
    }
}