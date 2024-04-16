using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRB;
    public GameObject player;
    public LayerMask playerLayer;

    public float health, speed, damage;

    public float followDistance;

    public void DetectPlayer(float followDistance, GameObject player)
    {
        // Just to see the ray
        Vector2 playerDirection = player.transform.position - transform.position;
        Ray2D rayToSee = new Ray2D(transform.position, playerDirection);
        Debug.DrawRay(rayToSee.origin, rayToSee.direction * followDistance, Color.green);
        
        RaycastHit2D hit = Physics2D.Raycast(rayToSee.origin, playerDirection, followDistance, playerLayer);
        if (hit.collider!=null)
        {
            Debug.Log("Collider "+hit.collider.name+" detected");
        }
    }
}