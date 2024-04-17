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

    public void TakeDamage(float damage, DamageType damageType)
    {
        float finalDamage = 0.0f;
        switch (damageType)
        {
            case DamageType.Physical:
                finalDamage = damage / 2.0f;
                base.ReduceHealth(finalDamage);
                break;
            case DamageType.Magical:
                finalDamage = damage * 2.0f;
                base.ReduceHealth(finalDamage);
                break;
            default:
                Debug.LogError("Damage type not found");
                break;
        }
    }
}