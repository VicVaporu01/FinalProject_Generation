using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject exitPortalPrefab;
    [SerializeField] private Transform spawnShopPoint;
    [SerializeField] private Transform spawnPortalPoint;

    public void SpawnShop()
    {
        Instantiate(shopPrefab, spawnShopPoint.position, Quaternion.identity);

        Instantiate(exitPortalPrefab, spawnPortalPoint.position, Quaternion.identity);
    }
}
