using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    private EnemyDistanceCombat distanceCombatController;
    [SerializeField] private GameObject aim;
    
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float minDistanceValue = 10.0f, scapeDistance = 3.0f;
    
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

        aim.transform.position = transform.position;
        
        // Calculate the direction to the player
        Vector2 directionToPlayer = GetPlayer().transform.position - transform.position;

        // Calculate the angle to the player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Rotate the archer to face the player
        aim.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
    }

    private void FixedUpdate()
    {
        Flip();
        CalculateApproach(minDistanceValue);

        if (canAttack && attackCooldown<=0)
        {
            distanceCombatController.Shoot();
            attackCooldown = 1.5f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

        float enemyPlayerDistance = Vector2.Distance(transform.position, GetPlayer().transform.position);
        if (enemyPlayerDistance <= scapeDistance)
        {
            Scape();
        }
        
    }

    private void Scape()
    {
        Vector2 scapeVector = transform.position - GetPlayer().transform.position;
        
        GetEnemyRB().velocity = scapeVector.normalized * speed;
    }
    
}