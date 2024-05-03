using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Warrior : Enemy
{
    private Animator animator;
    [SerializeField] private WeaponController sword;
    [SerializeField] private EnemyMeleeCombat meleeCombatController;

    [SerializeField] private float timeToAttack = 1.5f;
    [SerializeField] private float velocity;
    private int hash_isFacingRight, hash_velocity, hash_attacked, hash_hit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // followDistance = 2.0f;
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));
        meleeCombatController = GetComponent<EnemyMeleeCombat>();

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

        float distanceToPlayer = Vector2.Distance(transform.position, GetPlayer().transform.position);
        // This condition is to avoid the enemy's weapon getting mad when the player is too close
        if (hasLineOfSight)
        {
            sword.AimWeaponToPlayer("Warrior");
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


        if (hasLineOfSight && canAttack && timeToAttack <= 0)
        {
            GetEnemyRB().velocity = Vector2.zero;
            animator.SetBool(hash_hit, true);
            meleeCombatController.Hit();
            timeToAttack = 1.5f;
        }
        else if (timeToAttack >= 0)
        {
            animator.SetBool(hash_hit, false);
            timeToAttack -= Time.deltaTime;
        }

        CalculateApproach(minDistanceToAttack);
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
        // Debug.Log("Warrior CalculateFinalDamage");
        animator.SetTrigger(hash_attacked);
        switch (damageType)
        {
            case DamageType.Physical:
                return damage / 2.0f;
            case DamageType.Magical:
                return damage * 2.0f;
            default:
                Debug.LogError("Damage type not found");
                return 0f;
        }
    }
}