using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLife : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    [SerializeField] private float bulletVelocity;

    [SerializeField] private float lifeTime = 3.0f;

    private void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bulletVelocity = bulletRB.velocity.magnitude;
    }

    private void OnEnable()
    {
        lifeTime = 3.0f;
    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamage damage))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                damage.TakeDamage(1.0f, DamageType.Physical);
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}