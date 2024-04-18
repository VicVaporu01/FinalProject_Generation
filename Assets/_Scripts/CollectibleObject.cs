using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject: MonoBehaviour
{
    public int speedStats; // player speed
    public int damangeStats; // player damage 
    public int maxLifeStats; // Max player life


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStatistics = collision.GetComponent<PlayerStats>();
            playerStatistics.ModifyStatistics(speedStats, damangeStats, maxLifeStats);
            Destroy(gameObject); // Destroy this object when interacting with the player 

        }
    }
}
