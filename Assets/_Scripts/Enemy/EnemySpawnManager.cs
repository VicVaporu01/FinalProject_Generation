using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<Transform> spawnPoints;
    
    [SerializeField] private float spawnRate;
    
    private void Start()
    {
        SetSpawnRate(GameManager.Instance.GiveDifficultyToLevel());
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
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
        int randomEnemy = Random.Range(0, enemies.Length);
        int randomPosition = Random.Range(0, spawnPoints.Count);

        Debug.Log("Spawn Rate: "+spawnRate);
        Instantiate(enemies[randomEnemy], spawnPoints[randomPosition].position, Quaternion.identity);
    }
    
}
