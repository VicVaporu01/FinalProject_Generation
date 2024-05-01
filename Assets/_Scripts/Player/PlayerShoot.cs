using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
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
    [SerializeField] private float shootDelay;
    [SerializeField] private float timeBtwnShots;
    private float timeLastShot;

    [Header("Ammo Control")]
    [SerializeField] private int maxAmmoAmount;
    [SerializeField] private int actualAmmoAmount;
    [SerializeField] private float timeToChargeAmmo;
    [SerializeField] private float actualChargeTime;

    [Header("Shoot Position")]
    [SerializeField] private Vector3 rightShootPosition;
    [SerializeField] private Vector3 leftShootPosition;
    [SerializeField] private Vector3 backShootPosition;
    [SerializeField] private Vector3 frontShootPosition;

    private void Awake()
    {
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        shootAction.started += TryToShoot;
        playerStats.OnStatsChanged += ChangeBulletValues;
    }

    private void OnDisable()
    {
        shootAction.started -= TryToShoot;
        playerStats.OnStatsChanged -= ChangeBulletValues;
    }

    private void Start()
    {
        maxAmmoAmount = (int)MathF.Floor(GameManager.Instance.bulletAmountStats / 2f);

        actualBulletDamage = (int)MathF.Round(playerStats.GetNewStatValue(GameManager.Instance.magicDamageStats, maxBulletDamage, minBulletDamage));
    }

    private void ChangeBulletValues(int newSpeed, int newDamage, int newMaxLife, int newMagicDamage, int newBulletAmountStats)
    {
        maxAmmoAmount = (int)MathF.Floor(newBulletAmountStats / 2f);

        actualBulletDamage = (int)MathF.Round(playerStats.GetNewStatValue(newMagicDamage, maxBulletDamage, minBulletDamage));

        if (actualAmmoAmount > maxAmmoAmount)
        {
            actualAmmoAmount = maxAmmoAmount;
        }
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

            Invoke(nameof(Shoot), shootDelay);
        }
    }

    public void Shoot()
    {
        SetShootTransform();

        PlayerBullet playerBullet = Instantiate(bulletPrefab, shootControllerTransform.position, shootControllerTransform.rotation);

        playerBullet.ChangeBulletDamage(actualBulletDamage);
    }

    private void SetShootTransform()
    {
        Vector2 lastPlayerMovement = playerMovement.GetLasMovement();

        shootControllerTransform.eulerAngles = new Vector3(0, 0, -90);

        shootControllerTransform.localPosition = frontShootPosition;

        if (MathF.Abs(lastPlayerMovement.x) > 0 && MathF.Abs(lastPlayerMovement.x) < 1)
        {
            if (lastPlayerMovement.x > 0)
            {
                shootControllerTransform.eulerAngles = new Vector3(0, 0, 0);
                shootControllerTransform.localPosition = rightShootPosition;
            }
            else
            {
                shootControllerTransform.eulerAngles = new Vector3(0, -180, 0);
                shootControllerTransform.localPosition = leftShootPosition;
            }
        }
        else
        {
            if (lastPlayerMovement.x < 0)
            {
                shootControllerTransform.eulerAngles = new Vector3(0, -180, 0);
                shootControllerTransform.localPosition = leftShootPosition;
            }
            else if (lastPlayerMovement.x > 0)
            {
                shootControllerTransform.eulerAngles = new Vector3(0, 0, 0);
                shootControllerTransform.localPosition = rightShootPosition;
            }

            if (lastPlayerMovement.y < 0)
            {
                shootControllerTransform.eulerAngles = new Vector3(0, 0, -90);
                shootControllerTransform.localPosition = backShootPosition;
            }
            else if (lastPlayerMovement.y > 0)
            {
                shootControllerTransform.eulerAngles = new Vector3(0, 0, 90);
                shootControllerTransform.localPosition = frontShootPosition;
            }
        }
    }


}
