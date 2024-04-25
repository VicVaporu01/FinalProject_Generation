using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Archer : Enemy
{
    private EnemyDistanceCombat distanceCombatController;

    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float attackRateTime = 1.5f;
    [SerializeField] private float scapeDistance = 3.0f;

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

        if (hasLineOfSight)
        {
            AimWeaponToPlayer();
        }
    }

    private void FixedUpdate()
    {
        Flip();
        CalculateApproach(minDistanceToAttack);

        if (canAttack && attackCooldown<=0)
        {
            distanceCombatController.Shoot();
            attackCooldown = attackRateTime;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

        // If the player is too close, scape and increase the attack rate time
        float enemyPlayerDistance = Vector2.Distance(transform.position, GetPlayer().transform.position);
        if (enemyPlayerDistance <= scapeDistance)
        {
            Scape();
            attackRateTime = 5.0f;
        }
        else
        {
            attackRateTime = 1.5f;
        }
        
    }

    private void Scape()
    {
        Vector2 scapeVector = transform.position - GetPlayer().transform.position;
        
        GetEnemyRB().velocity = scapeVector.normalized * speed;
    }
    
}