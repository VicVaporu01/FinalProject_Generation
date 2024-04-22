using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    private List<GameObject> enemyBulletList = new List<GameObject>();
    
    private int poolSize = 10;
    private void Start()
    {
        AddBulletToPool();
    }

    private void AddBulletToPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform);
            bullet.SetActive(false);
            
            enemyBulletList.Add(bullet);
        }
    }

    public GameObject RequestBullet()
    {
        foreach (GameObject bullet in enemyBulletList)
        {
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);

                return bullet;
            }
        }
        return null;
    }
    
}
