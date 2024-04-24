using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject: MonoBehaviour
{
    public int speedStats; // player speed
    public int damangeStats; // player damage 
    public int maxLifeStats; // Max player life
    public ParticleSystem selectionIndicator;
    private bool hasIndicator = false;
    public GameObject collectParticleObject;

    private void Start()
    {
        //selectionIndicator = GameObject.Find("SelectionIndicator").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected!");
            hasIndicator = true;

            if (selectionIndicator != null)
            {
                selectionIndicator.Play(true);
            }
            
        }
    }
    private void Update()
    {
        // Check if the "G" key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (hasIndicator)
        {
            PlayerStats playerStatistics = FindObjectOfType<PlayerStats>();
            playerStatistics.ModifyStatistics(speedStats, damangeStats, maxLifeStats);
            Instantiate(collectParticleObject, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destruye este objeto
        }
    }
}
