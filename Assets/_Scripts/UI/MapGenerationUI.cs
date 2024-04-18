using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerationUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapLevel firstStageLevel;
    [SerializeField] private MapLevel lastStageLevel;
    [SerializeField] private GameObject stageGameObject;
    [SerializeField] private GameObject levelConectorGameObject;
    [SerializeField] private List<MapLevel> levelTipeList;
    [SerializeField] private RectTransform containerRectTransform;

    [Header("Spawn Control")]
    [Range(3, 10)][SerializeField] private int stagesAmount;
    [Range(2, 3)][SerializeField] private int minimumLevelsByStage;
    [Range(3, 5)][SerializeField] private int maximumLevelsByStage;
    [SerializeField] private int stagesSpawned = 0;
    [SerializeField] private int lastStageLevelAmount;
    [SerializeField] private List<MapLevel> lastStageLevelsSpawned;
    [SerializeField] private List<MapLevel> allLevelsSpawned;

    private void Awake()
    {
        for (int i = 0; i < stagesAmount; i++)
        {
            SpawnStage();
        }
    }

    private void SpawnStage()
    {
        if (stagesSpawned == 0)
        {
            SpawnFirtsStage();
        }
        else if (stagesSpawned == (stagesAmount - 1))
        {
            SpawnLastStage();
        }
        else
        {
            SpawnRandomStage();
        }
    }

    private void SpawnFirtsStage()
    {
        GameObject stage = Instantiate(stageGameObject, containerRectTransform);

        MapLevel level = Instantiate(firstStageLevel, stage.transform);

        lastStageLevelsSpawned.Add(level);

        stagesSpawned++;
    }

    private void SpawnLastStage()
    {
        GameObject stage = Instantiate(stageGameObject, containerRectTransform);

        MapLevel level = Instantiate(lastStageLevel, stage.transform);

        foreach (MapLevel mapLevel in lastStageLevelsSpawned)
        {
            level.AddPreviousLevel(mapLevel);
        }

        allLevelsSpawned.Add(level);

        stagesSpawned++;
    }

    private void SpawnRandomStage()
    {
        GameObject stage = Instantiate(stageGameObject, containerRectTransform);

        stagesSpawned++;

        SpawnRandomLevelTypesInStage(stage.transform);
    }


    private void SpawnRandomLevelTypesInStage(Transform stage)
    {
        int randomLevelAmount = Random.Range(minimumLevelsByStage, maximumLevelsByStage + 1);

        List<MapLevel> lastStageListTemp = new();

        for (int i = 0; i < randomLevelAmount; i++)
        {
            int randomLevelTypeIndex = Random.Range(0, levelTipeList.Count);

            MapLevel levelSpawned = Instantiate(levelTipeList[randomLevelTypeIndex], stage);

            foreach (MapLevel mapLevel in lastStageLevelsSpawned)
            {
                mapLevel.AddNextLevel(levelSpawned);

                levelSpawned.AddPreviousLevel(mapLevel);
            }

            allLevelsSpawned.Add(levelSpawned);

            lastStageListTemp.Add(levelSpawned);
        }

        lastStageLevelsSpawned = lastStageListTemp;

        StartCoroutine(GenerateAllLevelLines());
    }

    private IEnumerator GenerateAllLevelLines()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (MapLevel mapLevel in allLevelsSpawned)
        {
            mapLevel.GenerateLines();
        }
    }

}
