using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;

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
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int TakeDamage(float damage, DamageType damageType)
    {
        currentHealth -= (int)damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        UIHealthController.Instance.UpdateHealthDisplay(currentHealth, maxHealth);

        return currentHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
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

}
