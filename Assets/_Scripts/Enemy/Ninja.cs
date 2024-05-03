using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ninja : Enemy
{
    private AIAgent aiAgent;
    private Animator animator;
    [SerializeField] private WeaponController knife;
    [SerializeField] private EnemyMeleeCombat meleeCombatController;
    private EnemyDistanceCombat distanceCombatController;

    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float attackRateTime = 1.5f;
    [SerializeField] private float maxMeleeCombat = 1.5f;
    [SerializeField] private float maxDistanceCombat = 4.0f;
    [SerializeField] private float velocity;

    private int hash_isFacingRight, hash_velocity, hash_attacked, hash_hit;

    private void Start()
    {
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));

        aiAgent = GetComponent<AIAgent>();
        animator = GetComponent<Animator>();
        meleeCombatController = GetComponent<EnemyMeleeCombat>();
        distanceCombatController = GetComponent<EnemyDistanceCombat>();

        //  Getting the hash of the parameters
        hash_isFacingRight = Animator.StringToHash("isFacingRight");
        hash_velocity = Animator.StringToHash("velocity");
        hash_attacked = Animator.StringToHash("attacked");
        hash_hit = Animator.StringToHash("hit");
    }

    private void Update()
    {
        DetectPlayer(followDistance, GetPlayer());

        if (hasLineOfSight)
        {
            knife.AimWeaponToPlayer("Ninja");
        }
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
            velocity = 0;
        }
        else
        {
            aiAgent.canMove = true;
            velocity = 1;
        }

        CalculateApproach(minDistanceToAttack);
        if (hasLineOfSight && canAttack && attackCooldown <= 0)
        {
            animator.SetBool(hash_hit, true);
            AttackPlayer();
            attackCooldown = attackRateTime;
        }   
        else if (attackCooldown > 0)
        {
            animator.SetBool(hash_hit, false);
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
        animator.SetTrigger(hash_attacked);
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
            meleeCombatController.Hit();
        }
        else
        {
            // Atacar a distancia
            distanceCombatController.Shoot("Ninja");
        }
    }
}