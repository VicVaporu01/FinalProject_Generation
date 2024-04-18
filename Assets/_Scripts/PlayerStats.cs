using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int minStatValue = 1;
    public int maxStatValue = 8;

    public int speedStats; // player speed
    public int damangeStats; // player damage 
    public int maxLifeStats; // player's maximun life

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerStats()
    {
        speedStats = minStatValue;
        damangeStats = minStatValue;
        maxLifeStats = minStatValue;

    }
    public void ModifyStatistics(int newSpeed, int newDamage, int newMaxLife)
    {
        newSpeed = Mathf.Max(Mathf.Min(newSpeed, maxStatValue), minStatValue);
        newDamage = Mathf.Max(Mathf.Min(newDamage, maxStatValue), minStatValue);
        newMaxLife = Mathf.Max(Mathf.Min(newMaxLife, maxStatValue), minStatValue);

        speedStats = Mathf.Min(speedStats + newSpeed, maxStatValue);
        damangeStats = Mathf.Min(damangeStats + newDamage, maxStatValue);
        maxLifeStats = Mathf.Min(maxLifeStats + newMaxLife, maxStatValue);
    }

}