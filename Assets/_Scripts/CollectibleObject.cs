using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class CollectibleObject : MonoBehaviour
{
    [Header("Object Collect Stats")]
    [SerializeField] private int speedStats;
    [SerializeField] private int damangeStats;
    [SerializeField] private int maxLifeStats;
    [SerializeField] private int magicDamageStats;
    [SerializeField] private int bulletAmountStats;

    [Header("Object Collect Effects")]
    [SerializeField] private ParticleSystem hoverIndicator;
    [SerializeField] private ParticleSystem spawnEffect;
    [SerializeField] private GameObject collectParticleObject;
    [SerializeField] private float timeToSpawnObject = 0.4f;

    [Header("Price Values")]
    [SerializeField] private int coinCost;
    [SerializeField] private bool isFree = true;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private GameObject textCostObject;

    [Header("References")]
    [SerializeField] private GameObject shadowGameObject;
    [SerializeField] private GameObject lightGameObject;
    private SpriteRenderer spriteRenderer;

    [Header("Reward Object")]
    [SerializeField] private ObjectSpawnManager objectSpawnManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke(nameof(EnableObjectVisuals), timeToSpawnObject);

        costText.text = coinCost.ToString();
    }

    private void EnableObjectVisuals()
    {
        spriteRenderer.enabled = true;

        shadowGameObject.SetActive(true);

        lightGameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hoverIndicator.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hoverIndicator.Stop();
        }
    }

    public void Collect()
    {
        CoinSystem playerCoinSystem = FindObjectOfType<CoinSystem>();
        PlayerStats playerStatistics = FindObjectOfType<PlayerStats>();

        if (isFree)
        {
            CollectObject(playerStatistics);
        }
        else
        {
            if (playerCoinSystem.currentCoins >= coinCost)
            {
                CollectObject(playerStatistics);
                playerCoinSystem.LoseCoins(coinCost);

            }
            else
            {
                Debug.Log("No tienes suficientes monedas para adquirir este objeto");
            }
        }
    }

    private void CollectObject(PlayerStats playerStatistics)
    {
        playerStatistics.ModifyStatistics(speedStats, damangeStats, maxLifeStats, magicDamageStats, bulletAmountStats);

        Instantiate(collectParticleObject, transform.position, Quaternion.identity);

        if (objectSpawnManager != null)
        {
            objectSpawnManager.RewardObjectCollected(this);
        }

        Destroy(gameObject);
    }

    public void DestroyCollectableObject()
    {
        Instantiate(collectParticleObject, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void ChangeObjectFreeValue(bool state)
    {
        isFree = state;

        if (!isFree)
        {
            textCostObject.SetActive(true);
        }
    }

    public void SetObjectRewardParent(ObjectSpawnManager objectSpawnManagerParameter)
    {
        objectSpawnManager = objectSpawnManagerParameter;
    }
}
