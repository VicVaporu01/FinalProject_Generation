using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;
    
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get => instance;
        private set => instance = value;
    }

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
    
}
