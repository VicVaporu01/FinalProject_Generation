using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private GameObject player;

    public float health, speed, damage;
    public bool hasLineOfSight = false;

    public float followDistance;
    private float timePatrolling;
    private Vector2 randomDirection;
    public float xRange, yRange;

    public void DetectPlayer(float followDistance, GameObject player)
    {
        // Just to see the ray
        Vector2 playerDirection = player.transform.position - transform.position;
        Ray2D rayToSee = new Ray2D(transform.position, playerDirection);
        Debug.DrawRay(rayToSee.origin, rayToSee.direction * followDistance, Color.green);

        RaycastHit2D[] hit = Physics2D.RaycastAll(rayToSee.origin, playerDirection, followDistance);
        if (hit.Length > 1 && hit[1].collider != null && hit[1].collider.CompareTag("Player"))
        {
            hasLineOfSight = true;
        }
        else
        {
            hasLineOfSight = false;
        }
    }

    public void FollowPlayer(GameObject player)
    {
        if (hasLineOfSight)
        {
            Vector2 playerDirection = player.transform.position - transform.position;
            enemyRB.velocity = playerDirection.normalized * speed;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    public void GenerateRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange)).normalized;
    }

    public void ReduceHealth(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public Rigidbody2D GetEnemyRB()
    {
        return enemyRB;
    }

    public void SetEnemyRB(Rigidbody2D rb)
    {
        enemyRB = rb;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject playerFound)
    {
        player = playerFound;
    }

    public float GetTimePatrolling()
    {
        return timePatrolling;
    }

    public void SetTimePatrolling(float time)
    {
        timePatrolling = time;
    }

    public Vector2 GetRandomDirection()
    {
        return randomDirection;
    }
}