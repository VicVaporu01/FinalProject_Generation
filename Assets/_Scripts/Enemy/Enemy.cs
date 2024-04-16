using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRB;
    public GameObject player;
    public LayerMask playerLayer;

    public float health, speed, damage;
    public bool hasLineOfSight = false;
    public float followDistance;

    public void DetectPlayer(float followDistance, GameObject player)
    {
        // Just to see the ray
        Vector2 playerDirection = player.transform.position - transform.position;
        Ray2D rayToSee = new Ray2D(transform.position, playerDirection);
        Debug.DrawRay(rayToSee.origin, rayToSee.direction * followDistance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(rayToSee.origin, playerDirection, followDistance, playerLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
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
}