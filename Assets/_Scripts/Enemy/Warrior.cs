using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Warrior : Enemy
{
    [SerializeField] private WeaponController sword;
    [SerializeField] private EnemyMeleeCombat meleeCombatController;

    [SerializeField] private float timeToAttack = 1.5f;

    private void Start()
    {
        // followDistance = 2.0f;
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));
        meleeCombatController = GetComponent<EnemyMeleeCombat>();
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, GetPlayer());
        base.FollowPlayer(GetPlayer());

        float distanceToPlayer = Vector2.Distance(transform.position, GetPlayer().transform.position);
        // This condition is to avoid the enemy's weapon getting mad when the player is too close
        if (hasLineOfSight)
        {
            sword.AimWeaponToPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (hasLineOfSight && canAttack && timeToAttack <= 0)
        {
            meleeCombatController.Hit();
            timeToAttack = 1.5f;
        }
        else if (timeToAttack >= 0)
        {
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