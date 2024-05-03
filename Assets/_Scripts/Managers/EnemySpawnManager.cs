using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private EnemiesPool enemiesPool;

    private float spawnRate;
    private int enemiesMaxAmount;
    private int enemiesInvoked;
    private int enemiesCurrentAmount;

    private void Start()
    {
        SetSpawnRate(GameManager.Instance.GiveDifficultyToLevel());
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);

        enemiesCurrentAmount = enemiesMaxAmount;
    }

    private void SetSpawnRate(MapLevelTypeEnum difficulty)
    {
        switch (difficulty)
        {
            case MapLevelTypeEnum.NormalLevel:
                Debug.Log("Normal Level");
                enemiesMaxAmount = 3;
                spawnRate = 3.0f;
                break;
            case MapLevelTypeEnum.MediumLevel:
                Debug.Log("Medium Level");
                enemiesMaxAmount = 4;
                spawnRate = 2.0f;
                break;
            case MapLevelTypeEnum.HardLevel:
                Debug.Log("Hard Level");
                enemiesMaxAmount = 5;
                spawnRate = 1.0f;
                break;
            case MapLevelTypeEnum.BossLevel:
                Debug.Log("Boss Level");
                enemiesMaxAmount = 10;
                spawnRate = 2.0f;
                break;
            case MapLevelTypeEnum.ShopLevel:
                Debug.Log("Shop Level");
                enemiesMaxAmount = 0;
                spawnRate = 100.0f;
                SpawnShopBehaviour();
                break;
            default:
                Debug.Log("Default Level");
                enemiesMaxAmount = 2;
                spawnRate = 4.0f;
                break;
        }

        enemiesPool.AddEnemiesToPool();
    }

    private void SpawnEnemy()
    {
        if (enemiesCurrentAmount > 0 && enemiesInvoked < enemiesMaxAmount)
        {
            enemiesInvoked += 1;
            int randomPosition = Random.Range(0, spawnPoints.Count);

            GameObject enemy = enemiesPool.RequestEnemy();

            if (enemy != null)
            {
                enemy.transform.position = spawnPoints[randomPosition].position;
            }
        }
        else
        {
            Debug.Log("No se pueden invocar más enemigos");
        }
    }

    public int GetEnemiesMaxAmount()
    {
        return enemiesMaxAmount;
    }

    public void SubtractEnemy()
    {
        if (enemiesCurrentAmount > 0)
        {
            enemiesCurrentAmount -= 1;

            if (enemiesCurrentAmount <= 0)
            {
                FindObjectOfType<ObjectSpawnManager>().SpawnRewardObjects();
            }
        }
    }

    private void SpawnShopBehaviour()
    {
        ShopSpawnManager shopSpawnManager = FindObjectOfType<ShopSpawnManager>();

        if (shopSpawnManager != null)
        {
            shopSpawnManager.SpawnShop();
        }
    }
}