using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<Transform> spawnPoints;
    
    [SerializeField] private float spawnRate = 2.0f;
    
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);
        int randomPosition = Random.Range(0, spawnPoints.Count);

        Instantiate(enemies[randomEnemy], spawnPoints[randomPosition].position, Quaternion.identity);
    }
    
}
