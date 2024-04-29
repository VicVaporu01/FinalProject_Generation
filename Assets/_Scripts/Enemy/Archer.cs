using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Archer : Enemy
{
    [SerializeField] private WeaponController bow;
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
            bow.AimWeaponToPlayer();
        }
    }

    private void FixedUpdate()
    {
        // To patrol
        if (!hasLineOfSight && timePatrolling >= 0)
        {
            Debug.Log("Patrullando");
            timePatrolling -= Time.deltaTime;
            GetEnemyRB().velocity = GetRandomDirection() * speed;
        }
        else
        {
            base.GenerateRandomDirection();
            timePatrolling = 5.0f;
        }

        if (hasLineOfSight)
        {
            Flip();
        }
        CalculateApproach(minDistanceToAttack);

        // If the enemy can attack, has line of sight and the attack cooldown is 0, then attack
        if (canAttack && hasLineOfSight && attackCooldown <= 0)
        {
            distanceCombatController.Shoot();
            attackCooldown = attackRateTime;
        }
        else if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        // If the player is too close, scape and increase the attack rate time
        float enemyPlayerDistance = Vector2.Distance(transform.position, GetPlayer().transform.position);
        if (hasLineOfSight && (enemyPlayerDistance <= scapeDistance))
        {
            Scape();
            attackRateTime = 5.0f;
        }
        else
        {
            attackRateTime = 1.5f;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GenerateRandomDirection();
        }
    }

    private void Scape()
    {
        Vector2 scapeVector = transform.position - GetPlayer().transform.position;

        GetEnemyRB().velocity = scapeVector.normalized * speed;
    }
}