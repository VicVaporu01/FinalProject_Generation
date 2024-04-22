using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    private Rigidbody2D enemyRB;
    private GameObject player;

    public float health, speed, damage;
    public bool hasLineOfSight = false, canApproach = false, canAttack = false;

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

    protected virtual void FollowPlayer(GameObject player)
    {
        if (hasLineOfSight && canApproach)
        {
            Vector2 playerDirection = player.transform.position - transform.position;
            enemyRB.velocity = playerDirection.normalized * speed;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    /*
    * This method is to calculate if the enemy can approach the player to
    * avoid being too close
    */
    public void CalculateApproach(float minApproach)
    {
        float distanceEnemyPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceEnemyPlayer > minApproach)
        {
            canApproach = true;
            canAttack = false;
        }
        else
        {
            canApproach = false;
            canAttack = true;
        }
    }

    public void GenerateRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange)).normalized;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        float finalDamage = CalculateFinalDamage(damage, damageType);
        ReduceHealth(finalDamage);
    }

    /*
     * This method is to calculate the final damage that the enemy will receive
     * and can be overriden by the child classes
    */ 
    public virtual float CalculateFinalDamage(float damage, DamageType damageType)
    {
        Debug.Log("Enemy CalculateFinalDamage");
        switch (damageType)
        {
            case DamageType.Physical:
                return damage;
            case DamageType.Magical:
                return damage;
            default:
                Debug.LogError("Damage type not found");
                return 0f;
        }
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