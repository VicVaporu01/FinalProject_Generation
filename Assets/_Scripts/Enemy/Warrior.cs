using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Warrior : Enemy, IDamage
{
    [SerializeField] private MeleeCombat meleeCombatController;

    [SerializeField] private float minDistanceValue;
    [SerializeField] private float timeToAttack = 1.5f;

    private void Start()
    {
        // followDistance = 2.0f;
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));
        meleeCombatController = GetComponent<MeleeCombat>();
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, GetPlayer());
        base.FollowPlayer(GetPlayer());
    }

    private void FixedUpdate()
    {
        // This is to make the enemy look at its walking direction
        if (GetEnemyRB().velocity != Vector2.zero)
        {
            Vector3 lookDirection = new Vector3(GetEnemyRB().velocity.x, GetEnemyRB().velocity.y, 0);
            transform.up = lookDirection;
        }

        if (canAttack && timeToAttack <= 0)
        {
            meleeCombatController.Hit();
            timeToAttack = 1.5f;
        }
        else if (timeToAttack >= 0)
        {
            timeToAttack -= Time.deltaTime;
        }

        CalculateApproach(minDistanceValue);
        if (!hasLineOfSight && GetTimePatrolling() >= 0)
        {
            // Debug.Log("Moviendose");
            SetTimePatrolling((GetTimePatrolling() - Time.deltaTime));
            GetEnemyRB().velocity = GetRandomDirection() * speed;
        }
        else
        {
            base.GenerateRandomDirection();
            SetTimePatrolling(5.0f);
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