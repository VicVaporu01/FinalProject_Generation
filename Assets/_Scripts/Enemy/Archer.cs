using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Archer : Enemy
{
    private Animator animator;
    [SerializeField] private WeaponController bow;
    private EnemyDistanceCombat distanceCombatController;

    [SerializeField] private float attackCooldown = 0;
    [SerializeField] private float attackRateTime = 1.5f;
    [SerializeField] private float scapeDistance = 3.0f;

    [SerializeField] private float velocity;

    private int hash_isFacingRight, hash_velocity, hash_attacked, hash_hit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));

        distanceCombatController = GetComponent<EnemyDistanceCombat>();

        //  Getting the hash of the parameters
        hash_isFacingRight = Animator.StringToHash("isFacingRight");
        hash_velocity = Animator.StringToHash("velocity");
        hash_attacked = Animator.StringToHash("attacked");
        hash_hit = Animator.StringToHash("hit");
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, GetPlayer());
        base.FollowPlayer(GetPlayer());

        if (hasLineOfSight)
        {
            bow.AimWeaponToPlayer("Archer");
        }
    }

    private void FixedUpdate()
    {
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

        CalculateApproach(minDistanceToAttack);
        // To patrol
        if (!hasLineOfSight && timePatrolling >= 0)
        {
            timePatrolling -= Time.deltaTime;
            GetEnemyRB().velocity = GetRandomDirection() * speed;
        }
        else
        {
            base.GenerateRandomDirection();
            timePatrolling = 5.0f;
        }

        // If the enemy can attack, has line of sight and the attack cooldown is 0, then attack
        if (canAttack && hasLineOfSight && attackCooldown <= 0)
        {
            GetEnemyRB().velocity = Vector2.zero;
            animator.SetBool(hash_hit, true);
            distanceCombatController.Shoot("Archer");
            attackCooldown = attackRateTime;
        }
        else if (attackCooldown > 0)
        {
            animator.SetBool(hash_hit, false);
            attackCooldown -= Time.deltaTime;
        }

        // If the player is too close, scape and increase the attack rate time
        float enemyPlayerDistance = Vector2.Distance(transform.position, GetPlayer().transform.position);
        if (hasLineOfSight && (enemyPlayerDistance <= scapeDistance))
        {
            Scape();
            attackRateTime = 3.0f;
        }
        else
        {
            attackRateTime = 1.5f;
        }

        velocity = enemyRB.velocity.magnitude;
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
        // Debug.Log("Archer CalculateFinalDamage");
        animator.SetTrigger(hash_attacked);
        switch (damageType)
        {
            case DamageType.Physical:
                return damage * 2.0f;
            case DamageType.Magical:
                return damage / 2.0f;
            default:
                Debug.LogError("Damage type not found");
                return 0f;
        }
    }

    private void Scape()
    {
        Vector2 scapeVector = transform.position - GetPlayer().transform.position;

        GetEnemyRB().velocity = scapeVector.normalized * speed;
    }
}