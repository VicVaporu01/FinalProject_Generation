using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Damage Values")]
    [SerializeField] private DamageType damageType;
    [SerializeField] private int bulletDamage;

    [Header("Movement Values")]
    [SerializeField] private float movementSpeed;

    private void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamage objectHit))
            {
                objectHit.TakeDamage(bulletDamage, damageType);
            }
        }
    }

    public void ChangeBulletDamage(int newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }
}
