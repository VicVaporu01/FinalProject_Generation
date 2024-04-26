using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private EnemiesPool enemiesPool;

    [SerializeField] private float spawnRate;
    [SerializeField] private int enemiesMaxAmount;
    [SerializeField] private int enemiesInvoked;
    [SerializeField] private int enemiesCurrentAmount;

    private void Start()
    {
        SetSpawnRate(GameManager.Instance.GiveDifficultyToLevel());
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
        enemiesPool = GameObject.Find("EnemiesPool").GetComponent<EnemiesPool>();

        enemiesCurrentAmount = enemiesMaxAmount;
    }

    private void SetSpawnRate(MapLevelTypeEnum difficulty)
    {
        switch (difficulty)
        {
            case MapLevelTypeEnum.NormalLevel:
                Debug.Log("Normal Level");
                spawnRate = 3.0f;
                break;
            case MapLevelTypeEnum.MediumLevel:
                Debug.Log("Medium Level");
                spawnRate = 2.0f;
                break;
            case MapLevelTypeEnum.HardLevel:
                Debug.Log("Hard Level");
                spawnRate = 1.0f;
                break;
            default:
                Debug.Log("Default Level");
                spawnRate = 4.0f;
                break;
        }
    }

    private void SpawnEnemy()
    {
        if (enemiesCurrentAmount > 0 && enemiesInvoked < enemiesMaxAmount)
        {
            enemiesInvoked += 1;
            int randomPosition = Random.Range(0, spawnPoints.Count);

            Debug.Log("Spawn Rate: " + spawnRate);
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
}