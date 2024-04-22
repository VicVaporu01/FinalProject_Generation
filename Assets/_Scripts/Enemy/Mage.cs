using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mage : Enemy
{
    [SerializeField] private EnemyDistanceCombat distanceCombatController;
    [SerializeField] private float minDistanceValue, tpCooldown = 3.0f;
    [SerializeField] private float attackCooldown = 0;
    private void Start()
    {
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));
        distanceCombatController = GetComponentInChildren<EnemyDistanceCombat>();
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, GetPlayer());
        base.FollowPlayer(GetPlayer());
    }

    private void FixedUpdate()
    {
        CalculateApproach(minDistanceValue);
        if (health<= 2.5f && !canApproach && tpCooldown<=0)
        {
            Debug.Log("Teleporting");
            TeleportAway(minDistanceValue);
            tpCooldown = 3.0f;
        }
        else
        {
            tpCooldown -= Time.deltaTime;
        }
        
        // This is to make the enemy look at its walking direction
        if (GetEnemyRB().velocity != Vector2.zero)
        {
            Vector3 lookDirection = new Vector3(GetEnemyRB().velocity.x, GetEnemyRB().velocity.y, 0);
            transform.up = lookDirection;
        }
        
        if (canAttack && attackCooldown<=0)
        {
            distanceCombatController.Shoot();
            attackCooldown = 1.5f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void TeleportAway(float distance)
    { 
        transform.position = 
            new Vector3(transform.position.x + Random.Range(-distance, distance), 
                transform.position.y + Random.Range(-distance, distance), 0);
    }
    
}