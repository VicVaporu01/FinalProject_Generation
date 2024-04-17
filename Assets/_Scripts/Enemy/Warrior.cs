using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy, IDamage
{
    private void Start()
    {
        // followDistance = 2.0f;
        SetEnemyRB(GetComponent<Rigidbody2D>());
        SetPlayer(GameObject.Find("Player"));
    }

    private void Update()
    {
        base.DetectPlayer(followDistance, GetPlayer());
        base.FollowPlayer(GetPlayer());
    }

    private void FixedUpdate()
    {
        if (!hasLineOfSight && GetTimePatrolling() >= 0)
        {
            Debug.Log("Moviendose");
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
        Debug.Log("Warrior CalculateFinalDamage");
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