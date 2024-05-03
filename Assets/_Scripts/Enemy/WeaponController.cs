using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Animator weaponAnimator;

    [SerializeField] private GameObject character;
    private Enemy enemyScript;
    [SerializeField] private Transform aim;

    private Quaternion initialRotation;
    private int hash_hit;

    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        enemyScript = character.GetComponent<Enemy>();

        hash_hit = Animator.StringToHash("hit");
        initialRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (!enemyScript.hasLineOfSight)
        {
            aim.transform.position = character.transform.position;
        }
    }

    public void AimWeaponToPlayer(string enemy)
    {
        // Calculate the direction to the player
        Vector2 playerDirection = enemyScript.GetPlayer().transform.position - aim.transform.position;

        switch (enemy)
        {
            case "Ninja":
                if (!enemyScript.isFacingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    aim.transform.position = character.transform.position;

                    aim.transform.right = playerDirection;
                }
                else
                {
                    transform.rotation = initialRotation;
                    aim.transform.position = character.transform.position;

                    aim.transform.right = playerDirection;
                }

                break;
            default:
                // transform.rotation = initialRotation;
                aim.transform.position = character.transform.position;

                aim.transform.right = playerDirection;
                break;
        }
    }

    public void ActivateHitAnimation()
    {
        weaponAnimator.SetTrigger(hash_hit);
    }
}