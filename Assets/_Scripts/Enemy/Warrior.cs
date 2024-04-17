using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    private void Start()
    {
        // followDistance = 2.0f;
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, player);
        base.FollowPlayer(player);
    }

    private void FixedUpdate()
    {
        if (!hasLineOfSight && timePatrolling >= 0)
        {
            Debug.Log("Moviendose");
            timePatrolling -= Time.deltaTime;
            enemyRB.velocity = randomDirection * speed;
        }
        else
        {
            base.GenerateRandomDirection();
            timePatrolling = 5.0f;
        }
    }
}