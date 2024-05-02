using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ninja : Enemy
{
    private AIAgent aiAgent;
    private Animator animator;
    
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float attackRateTime = 1.5f;
    [SerializeField] private float maxMeleeCombat = 1.5f;
    [SerializeField] private float maxDistanceCombat = 4.0f;
    [SerializeField] private float velocity;
    
    private int hash_isFacingRight, hash_velocity, hash_attacked;

    private void Start()
    {
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));

        aiAgent = GetComponent<AIAgent>();
        animator = GetComponent<Animator>();
        
        //  Getting the hash of the parameters
        hash_isFacingRight = Animator.StringToHash("isFacingRight");
        hash_velocity = Animator.StringToHash("velocity");
        hash_attacked = Animator.StringToHash("attacked");
    }

    private void Update()
    {
        DetectPlayer(followDistance, GetPlayer());
    }

    private void FixedUpdate()
    {
        // To know if the enemy is facing right or left
        if (hasLineOfSight)
        {
            Flip();
        }
        
        // To set the animator parameter
        animator.SetFloat(hash_velocity, velocity);
        if (isFacingRight)
        {
            animator.SetBool(hash_isFacingRight, true);
        }
        else
        {
            animator.SetBool(hash_isFacingRight, false);
        }
        
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