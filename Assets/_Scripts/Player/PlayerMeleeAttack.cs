using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit the enemy: " + other.gameObject.name);
            IDamage objectHit = other.gameObject.GetComponent<IDamage>();
            if (objectHit != null)
            {
                Debug.Log("Haciendo daño: 1 " + DamageType.Physical);
                objectHit.TakeDamage(1, DamageType.Physical);
            }
        }
    }
}