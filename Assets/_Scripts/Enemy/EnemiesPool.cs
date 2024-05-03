using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> listEnemiesPrefabs;
    private GameObject[] enemyArray;

    private int arraySize;

    public void AddEnemiesToPool()
    {
        arraySize = GameObject.Find("SpawnManager").GetComponent<EnemySpawnManager>().GetEnemiesMaxAmount();
        enemyArray = new GameObject[arraySize];

        for (int i = 0; i < enemyArray.Length; i++)
        {
            int randomEnemy = Random.Range(0, listEnemiesPrefabs.Count);
            GameObject enemy = Instantiate(listEnemiesPrefabs[randomEnemy], transform);
            enemy.SetActive(false);

            enemyArray[i] = enemy;
        }
    }

    public GameObject RequestEnemy()
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (!enemyArray[i].activeSelf)
            {
                enemyArray[i].SetActive(true);

                return enemyArray[i];
            }
        }

        return null;
    }
}