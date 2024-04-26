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
            IDamage objectHit = other.gameObject.GetComponent<IDamage>();
            if (objectHit != null)
            {
                objectHit.TakeDamage(1, DamageType.Physical);
            }
        }
    }
}