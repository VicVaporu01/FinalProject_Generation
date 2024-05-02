using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Animator weaponAnimator;
    
    [SerializeField] private GameObject character;
    [SerializeField] private Transform aim;
    [SerializeField] private Enemy enemyScript;

    private int hash_hit;

    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        
        hash_hit = Animator.StringToHash("hit");
    }

    private void FixedUpdate()
    {
        if (!enemyScript.hasLineOfSight)
        {
            aim.transform.position = character.transform.position;
        }
    }

    public void AimWeaponToPlayer()
    {
        aim.transform.position = character.transform.position;

        // Calculate the direction to the player
        Vector2 playerDirection = enemyScript.GetPlayer().transform.position - aim.transform.position;

        aim.transform.right = playerDirection;
    }

    public void ActivateHitAnimation()
    {
        weaponAnimator.SetTrigger(hash_hit);
    }
    
}