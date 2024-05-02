using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ninja : Enemy
{
    private AIAgent aiAgent;

    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float attackRateTime = 1.5f;
    [SerializeField] private float maxMeleeCombat = 1.5f;
    [SerializeField] private float maxDistanceCombat = 4.0f;

    private void Start()
    {
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));

        aiAgent = GetComponent<AIAgent>();
    }

    private void Update()
    {
        DetectPlayer(followDistance, GetPlayer());
    }

    private void FixedUpdate()
    {
        if (hasLineOfSight)
        {
            aiAgent.canMove = false;
        }
        else
        {
            aiAgent.canMove = true;
        }

        CalculateApproach(minDistanceToAttack);
        if (hasLineOfSight && canAttack && attackCooldown <= 0)
        {
            AttackPlayer();
            attackCooldown = attackRateTime;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GenerateRandomDirection();
        }
    }

    public override float CalculateFinalDamage(float damage, DamageType damageType)
    {
        // Debug.Log("Ninja CalculateFinalDamage");

        switch (damageType)
        {
            case DamageType.Physical:
                return damage / 2.0f;
            case DamageType.Magical:
                return damage / 2.0f;
            default:
                Debug.LogError("Damage type not found");
                return 0f;
        }
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GetPlayer().transform.position);
        if (distanceToPlayer <= maxMeleeCombat)
        {
            // Atacar cuerpo a cuerpo
            Debug.Log("Atacar cuerpo a cuerpo");
        }
        else
        {
            // Atacar a distancia
            Debug.Log("Atacar a distancia");
        }
    }
}