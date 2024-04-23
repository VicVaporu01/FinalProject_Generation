using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<Transform> spawnPoints;
    
    [SerializeField] private Difficulty currentDifficulty;
    [SerializeField] private float spawnRate;
    
    private void Start()
    {
        SetSpawnRate(currentDifficulty);
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
    }
    
    public void SetDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
    }
    
    private void SetSpawnRate(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                spawnRate = 3.0f;
                break;
            case Difficulty.Medium:
                spawnRate = 2.0f;
                break;
            case Difficulty.Hard:
                spawnRate = 1.0f;
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

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
