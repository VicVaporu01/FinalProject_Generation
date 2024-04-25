using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDistanceCombat : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    private EnemyBulletPool enemyBulletPool;

    [SerializeField] private float bulletVelocity =2.0f;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyBulletPool = GameObject.Find("ArcherBulletPool").GetComponent<EnemyBulletPool>();
    }

    public void Shoot()
    {
        GameObject bullet = enemyBulletPool.RequestBullet();
        bullet.transform.position = shootPoint.position;
        
        // Calculate the direction of the bullet and shoot it
        Vector2 direction = player.position - shootPoint.position;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletVelocity;
    }
}
