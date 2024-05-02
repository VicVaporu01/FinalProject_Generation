using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private Transform[] spawnItemPoints;

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        foreach (Transform item in spawnItemPoints)
        {
            CollectibleObject collectibleObject = Instantiate(GameManager.Instance.GetRandomObjectToSpawn(), item.position, item.rotation);

            collectibleObject.ChangeObjectFreeValue(false);
        }
    }

}
