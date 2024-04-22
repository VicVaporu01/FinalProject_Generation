using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLife : MonoBehaviour
{

    [SerializeField] private float lifeTime=3.0f;

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
}
