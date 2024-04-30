using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;
    private InputAction shootAction;

    [Header("Damage Values")]
    [SerializeField] private int actualBulletDamage;
    [SerializeField] private int maxBulletDamage;
    [SerializeField] private int minBulletDamage;

    [Header("Shoot Control")]
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private Transform shootControllerTransform;
    [SerializeField] private float timeBtwnShots;
    private float timeLastShot;

    [Header("Ammo Control")]
    [SerializeField] private int maxAmmoAmount;
    [SerializeField] private int actualAmmoAmount;
    [SerializeField] private float timeToChargeAmmo;
    [SerializeField] private float actualChargeTime;

    private void Awake()
    {
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        shootAction.started += TryToShoot;
    }

    private void OnDisable()
    {
        shootAction.started -= TryToShoot;
    }

    private void Update()
    {
        if (actualAmmoAmount < maxAmmoAmount)
        {
            actualChargeTime += Time.deltaTime;

            if (actualChargeTime > timeToChargeAmmo)
            {
                actualAmmoAmount++;

                actualChargeTime = 0;
            }
        }
    }

    private void TryToShoot(InputAction.CallbackContext context)
    {
        if (actualAmmoAmount > 0 && Time.time > timeLastShot + timeBtwnShots)
        {
            actualAmmoAmount--;

            timeLastShot = Time.time;

            animator.SetTrigger("RangeAttack");
        }
    }

    public void Shoot()
    {
        PlayerBullet playerBullet = Instantiate(bulletPrefab, shootControllerTransform.position, shootControllerTransform.rotation);

        playerBullet.ChangeBulletDamage(actualBulletDamage);
    }


}
