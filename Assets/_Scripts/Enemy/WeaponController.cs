using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Transform aim;
    [SerializeField] private Enemy enemyScript;

    private void FixedUpdate()
    {
        if (!enemyScript.hasLineOfSight)
        {
            aim.transform.position = character.transform.position;
        }
    }

    // public void AimWeaponToPlayer()
    // {
    //     aim.transform.position = character.transform.position;
    //
    //     // Calculate the direction to the player
    //     Vector2 directionToPlayer = enemyScript.GetPlayer().transform.position - transform.position;
    //
    //     // Calculate the angle to the player
    //     float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    //
    //     // Rotate the archer to face the player
    //     aim.rotation = Quaternion.Euler(0, 0, angleToPlayer);
    // }
    
    public void AimWeaponToPlayer()
    {
        aim.transform.position = character.transform.position;

        // Calculate the direction to the player
        Vector2 playerDirection = enemyScript.GetPlayer().transform.position - aim.transform.position;

        aim.transform.right = playerDirection;
    }
    
}