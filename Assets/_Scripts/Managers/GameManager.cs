using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int minStatValue;
    public int speedStats;
    public int damangeStats;
    public int maxLifeStats;
    public int magicDamageStats;
    public int bulletAmountStats;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    private static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    public CollectibleObject[] collectableObjects;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        SetInitialStatsValues();
    }

    private void Start()
    {
        currentHealth = GetMaxHealth();
    }

    public int TakeDamage(float damage, DamageType damageType)
    {
        currentHealth -= (int)damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        UIHealthController.Instance.UpdateHealthDisplay(currentHealth, GetMaxHealth());

        return currentHealth;
    }

    public int HealDamage(int healAmount)
    {
        int temporalCurrentHealth = currentHealth + healAmount;
        int actualMaxHealth = GetMaxHealth();

        if (temporalCurrentHealth > actualMaxHealth)
        {
            currentHealth = actualMaxHealth;
        }
        else
        {
            currentHealth = temporalCurrentHealth;
        }

        UIHealthController.Instance.UpdateHealthDisplay(currentHealth, actualMaxHealth);

        return currentHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth + (maxLifeStats * 2);
    }

    // public void Set CurrentHealth(int health)
    // {
    //     currentHealth = health;
    // }

    public MapLevelTypeEnum GiveDifficultyToLevel()
    {
        return MapUIManager.Instance.actualMapLevel.mapLevelType;
    }

    public CollectibleObject GetRandomObjectToSpawn()
    {
        int randomObjectIndex = Random.Range(0, collectableObjects.Length);

        return collectableObjects[randomObjectIndex];
    }

    private void SetInitialStatsValues()
    {
        speedStats = minStatValue;
        damangeStats = minStatValue;
        maxLifeStats = minStatValue;
        magicDamageStats = minStatValue;
        bulletAmountStats = minStatValue;
    }

    public void ChangeStatsValues(int newSpeed, int newDamage, int newMaxLife, int newMagicDamage, int newBulletAmountStats)
    {
        speedStats = newSpeed;
        damangeStats = newDamage;
        maxLifeStats = newMaxLife;
        magicDamageStats = newMagicDamage;
        bulletAmountStats = newBulletAmountStats;
    }

    public int ChangePlayerMaxHealth(int newMaxHealth)
    {
        int maxLifeDiference = newMaxHealth - maxLifeStats;

        maxLifeStats = newMaxHealth;

        if (maxLifeDiference > 0)
        {
            currentHealth += maxLifeDiference * 2;
        }
        else
        {
            if (currentHealth > GetMaxHealth())
            {
                currentHealth = GetMaxHealth();
            }
        }

        UIHealthController.Instance.ChangeHearths(GetMaxHealth());

        UIHealthController.Instance.UpdateHealthDisplay(currentHealth, GetMaxHealth());

        return currentHealth;
    }

}
