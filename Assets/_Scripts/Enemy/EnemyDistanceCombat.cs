using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDistanceCombat : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    private EnemyBulletPool archerBulletPool;
    private EnemyBulletPool ninjaBulletPool;

    [SerializeField] private float bulletVelocity = 2.0f;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        archerBulletPool = GameObject.Find("ArcherBulletPool").GetComponent<EnemyBulletPool>();
        ninjaBulletPool = GameObject.Find("NinjaBulletPool").GetComponent<EnemyBulletPool>();
    }

    public void Shoot(string enemyType)
    {
        // Calculate the direction of the bullet and shoot it
        Vector2 direction = player.position - shootPoint.position;
        switch (enemyType)
        {
            case "Ninja":
                GameObject ninjaBullet = ninjaBulletPool.RequestBullet();
                ninjaBullet.transform.position = shootPoint.position;

                ninjaBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletVelocity;
                break;
            case "Archer":
                GameObject archerBullet = archerBulletPool.RequestBullet();
                archerBullet.transform.position = shootPoint.position;
                archerBullet.transform.rotation = aim.transform.rotation;

                archerBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletVelocity;
                break;
            default:
                Debug.LogError("Invalid enemy type.");
                break;
        }
    }
}