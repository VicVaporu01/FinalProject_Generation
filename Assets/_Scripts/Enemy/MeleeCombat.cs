using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRatio;
    // [SerializeField] private float attackDamage;

    public void Hit()
    {
        // Debug.Log("Hitting player");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackController.position, attackRatio);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.CompareTag("Player"))
            {
                // Debug.Log("collider: " + collider.name);
                IDamage objectHit = collider.GetComponent<IDamage>();
                if (objectHit != null)
                {
                    objectHit.TakeDamage(1, DamageType.Physical);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackController.position, attackRatio);
    }
}