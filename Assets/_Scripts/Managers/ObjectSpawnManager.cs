using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [Header("Object Reward Spawn")]
    [SerializeField] private Transform[] objectSpawnPositions;
    [SerializeField] private List<CollectibleObject> objectsSpawned;

    [Header("Exit Portal")]
    [SerializeField] private GameObject exitPortalPrefab;
    [SerializeField] private Transform exitPortalSpawnPosition;

    public void SpawnRewardObjects()
    {
        if (GameManager.Instance.GiveDifficultyToLevel() == MapLevelTypeEnum.BossLevel)
        {
            CollectibleObject finalObject = Instantiate(GameManager.Instance.GetFinalReward(), objectSpawnPositions[1].position, Quaternion.identity);

            finalObject.SetObjectRewardParent(this);

            objectsSpawned.Add(finalObject);
        }
        else
        {
            foreach (Transform spawnTransform in objectSpawnPositions)
            {
                CollectibleObject randomObject = Instantiate(GameManager.Instance.GetRandomObjectToSpawn(), spawnTransform.position, Quaternion.identity);

                randomObject.SetObjectRewardParent(this);

                objectsSpawned.Add(randomObject);
            }
        }
    }

    public void RewardObjectCollected(CollectibleObject collectibleObjectPicked)
    {
        foreach (CollectibleObject CollectibleObject in objectsSpawned)
        {
            if (collectibleObjectPicked != CollectibleObject)
            {
                CollectibleObject.DestroyCollectableObject();
            }
        }

        objectsSpawned.Clear();

        SpawnExitPortal();
    }

    private void SpawnExitPortal()
    {
        Instantiate(exitPortalPrefab, exitPortalSpawnPosition.position, Quaternion.identity);
    }
}
