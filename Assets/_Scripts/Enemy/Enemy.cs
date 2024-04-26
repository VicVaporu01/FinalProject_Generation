using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    private Rigidbody2D enemyRB;
    private GameObject player;
    [SerializeField] private GameObject aim, father;

    [Header("Enemy Stats")] 
    public float health;
    public float speed;
    public float damage;
    public bool hasLineOfSight = false, isFacingRight = true, 
        canApproach = false, canAttack = false;

    public float followDistance, minDistanceToAttack;
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

    public void AimWeaponToPlayer()
    {
        aim.transform.position = transform.position;
        
        // Calculate the direction to the player
        Vector2 directionToPlayer = GetPlayer().transform.position - transform.position;

        // Calculate the angle to the player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Rotate the archer to face the player
        aim.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
    }

    // This method is to flip the enemy when the player is on the opposite side
    public void Flip()
    {
        Vector2 playerPosition = player.transform.position;
        bool isPlayerRight = playerPosition.x > transform.position.x;
        
        // Is the player right and the enemy is not facing right, the enemy will flip
        // Or if the player is left and the enemy is facing right, the enemy will flip
        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            transform.Rotate(0,180,0);
            isFacingRight = !isFacingRight;
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
        GameObject.Find("SpawnManager").GetComponent<EnemySpawnManager>().SubtractEnemy();
        father.SetActive(false);
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