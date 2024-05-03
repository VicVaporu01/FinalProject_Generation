using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapGenerationUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapLevel firstStageLevel;
    [SerializeField] private MapLevel lastStageLevel;
    [SerializeField] private MapStage stageGameObject;
    [SerializeField] private RectTransform containerRectTransform;
    [SerializeField] private CanvasScaler canvasScaler;

    [Header("Level Spawn Controll")]
    [SerializeField] private List<MapLevel> levelTipeList;
    [Range(0, 100)][SerializeField] private float shopSpawnProbability;
    [Range(0, 100)][SerializeField] private float hardLevelProbability;
    [Range(0, 100)][SerializeField] private float mediumLevelProbability;

    [Header("Spawn Control")]
    [Range(3, 20)][SerializeField] private int stagesAmount;
    [Range(2, 3)][SerializeField] private int minimumLevelsByStage;
    [Range(3, 5)][SerializeField] private int maximumLevelsByStage;
    [Range(0, 100)][SerializeField] private float randomConectionProbability;
    private List<MapLevel> allLevelsSpawned = new();
    private List<MapLevel> lastStageLevelsSpawned = new();
    private int stagesSpawned;
    private float multiplyFactor;

    private void Awake()
    {
        GetMultiplyFactor();

        SpawnAllStages();
    }

    private void GetMultiplyFactor()
    {
        Vector2 referenceResolution = canvasScaler.referenceResolution;

        if (referenceResolution.x > referenceResolution.y)
        {
            multiplyFactor = referenceResolution.x / Screen.width;
        }
        else
        {
            multiplyFactor = referenceResolution.y / Screen.height;
        }
    }

    private void SpawnAllStages()
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
        MapStage stage = Instantiate(stageGameObject, containerRectTransform);

        MapLevel level = Instantiate(firstStageLevel, stage.transform);

        stage.AddMapLevelToStage(level);

        allLevelsSpawned.Add(level);

        lastStageLevelsSpawned.Add(level);

        MapUIManager.Instance.AddSpawnedStage(stage);

        MapUIManager.Instance.SetActualMapLevel(level);

        EventSystem.current.SetSelectedGameObject(level.gameObject);

        level.ActivateLevel();

        stagesSpawned++;
    }

    private void SpawnLastStage()
    {
        MapStage stage = Instantiate(stageGameObject, containerRectTransform);

        MapLevel level = Instantiate(lastStageLevel, stage.transform);

        stage.AddMapLevelToStage(level);

        foreach (MapLevel mapLevel in lastStageLevelsSpawned)
        {
            mapLevel.AddNextLevel(level);

            level.AddPreviousLevel(mapLevel);
        }

        allLevelsSpawned.Add(level);

        MapUIManager.Instance.AddSpawnedStage(stage);

        stagesSpawned++;
    }

    private void SpawnRandomStage()
    {
        MapStage stage = Instantiate(stageGameObject, containerRectTransform);

        MapUIManager.Instance.AddSpawnedStage(stage);

        stagesSpawned++;

        SpawnRandomLevelTypesInStage(stage);
    }


    private void SpawnRandomLevelTypesInStage(MapStage stage)
    {
        int randomLevelAmount = Random.Range(minimumLevelsByStage, maximumLevelsByStage + 1);

        List<MapLevel> lastStageListTemp = new();

        for (int i = 0; i < randomLevelAmount; i++)
        {
            int randomLevelTypeIndex = RandomLevelSpawnIndex();

            MapLevel levelSpawned = Instantiate(levelTipeList[randomLevelTypeIndex], stage.transform);

            stage.AddMapLevelToStage(levelSpawned);

            SetLevelConectors(levelSpawned, randomLevelAmount, i);

            allLevelsSpawned.Add(levelSpawned);

            lastStageListTemp.Add(levelSpawned);
        }

        lastStageLevelsSpawned = lastStageListTemp;

        StartCoroutine(GenerateAllLevelLines());
    }

    private int RandomLevelSpawnIndex()
    {
        float randomLevelProbability = Random.Range(0, 100);

        if (randomLevelProbability < shopSpawnProbability)
        {
            return 1;
        }
        else if (randomLevelProbability >= shopSpawnProbability && randomLevelProbability < shopSpawnProbability + hardLevelProbability)
        {
            return 2;
        }
        else if (randomLevelProbability >= shopSpawnProbability + hardLevelProbability && randomLevelProbability < shopSpawnProbability + hardLevelProbability + mediumLevelProbability)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    private void SetLevelConectors(MapLevel levelSpawned, int levelsToSpawn, int levelSpawnIndex)
    {
        if (stagesSpawned == 2)
        {
            foreach (MapLevel mapLevel in lastStageLevelsSpawned)
            {
                mapLevel.AddNextLevel(levelSpawned);

                levelSpawned.AddPreviousLevel(mapLevel);
            }
        }
        else
        {
            for (int i = 0; i < lastStageLevelsSpawned.Count; i++)
            {
                if (CanCreateConection(lastStageLevelsSpawned.Count, levelsToSpawn, i, levelSpawnIndex))
                {

                    if ((levelSpawnIndex == 0 && i == 0) || (levelSpawnIndex == levelsToSpawn - 1 && i == lastStageLevelsSpawned.Count - 1))
                    {
                        CreateConectionBtwn(i, levelSpawned);
                    }
                    else
                    {
                        bool hasNextLevels = HaveNextLevels(lastStageLevelsSpawned[i]);
                        bool hasPreviousLevels = HavePreviousLevels(levelSpawned);
                        bool isTheLastSpawnLevel = IsTheLastLevelToSpawn(levelsToSpawn, levelSpawnIndex);
                        bool isTheLastPreviousLevel = IsTheLastPreviousLevel(i);

                        if (CanCreateFutureConections(lastStageLevelsSpawned.Count, levelsToSpawn, i, levelSpawnIndex))
                        {
                            if ((!hasNextLevels && isTheLastSpawnLevel) || (!hasPreviousLevels && isTheLastPreviousLevel))
                            {
                                CreateConectionBtwn(i, levelSpawned);
                            }
                            else
                            {
                                CreateRandomConection(i, levelSpawned);
                            }
                        }
                        else
                        {
                            CreateConectionBtwn(i, levelSpawned);
                        }
                    }
                }
            }
        }
    }

    private bool CanCreateFutureConections(int leftAmount, int rightAmount, int indexLeft, int indexRight)
    {
        int futureConectionsAmount = 0;

        for (int i = indexRight; i < rightAmount; i++)
        {
            if (CanCreateConection(leftAmount, rightAmount, indexLeft, i))
            {
                futureConectionsAmount++;
            }
        }

        if (futureConectionsAmount > 1)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    private bool CanCreateConection(int leftAmount, int rightAmount, float indexLeft, float indexRight)
    {
        int difference = leftAmount - rightAmount;

        float delta = Mathf.Abs(difference / 2f);

        if (delta == 0)
        {
            delta = 1;
        }

        if (difference < 0)
        {
            indexLeft += Mathf.Abs(difference / 2f);
        }
        else
        {
            indexRight += Mathf.Abs(difference / 2f);
        }

        if (MathF.Abs(indexLeft - indexRight) <= delta)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HaveNextLevels(MapLevel mapLevel)
    {
        return mapLevel.NextLevels.Any();
    }

    private bool HavePreviousLevels(MapLevel mapLevel)
    {
        return mapLevel.PreviousLevels.Any();
    }

    private bool IsTheLastLevelToSpawn(int levelsToSpawn, int levelSpawnIndex)
    {
        if (levelsToSpawn - 1 - levelSpawnIndex == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTheLastPreviousLevel(int actualIndex)
    {
        if (lastStageLevelsSpawned.Count - 1 - actualIndex == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CreateConectionBtwn(int lastLevelIndex, MapLevel levelSpawned)
    {
        lastStageLevelsSpawned[lastLevelIndex].AddNextLevel(levelSpawned);

        levelSpawned.AddPreviousLevel(lastStageLevelsSpawned[lastLevelIndex]);
    }

    private void CreateRandomConection(int lastLevelIndex, MapLevel levelSpawned)
    {
        float randomProbability = Random.Range(0, 100);

        if (randomProbability <= randomConectionProbability)
        {
            CreateConectionBtwn(lastLevelIndex, levelSpawned);
        }
    }

    private IEnumerator GenerateAllLevelLines()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (MapLevel mapLevel in allLevelsSpawned)
        {
            mapLevel.GenerateLines(multiplyFactor);
        }
    }
}