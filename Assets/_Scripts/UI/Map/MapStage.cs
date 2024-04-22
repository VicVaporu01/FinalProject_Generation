using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStage : MonoBehaviour
{
    [SerializeField] private List<MapLevel> mapLevelsInStage;

    public void AddMapLevelToStage(MapLevel mapLevelCreated)
    {
        mapLevelsInStage.Add(mapLevelCreated);
    }

    public void DisableAllMapLevelsInStage()
    {
        foreach (MapLevel mapLevel in mapLevelsInStage)
        {
            mapLevel.DisableMapLevel();
        }
    }

    public void DisableAllMapLevelsInStageExcept(MapLevel mapLevelParameter)
    {
        foreach (MapLevel mapLevel in mapLevelsInStage)
        {
            if (mapLevel != mapLevelParameter)
            {
                mapLevel.DisableMapLevel();
            }
        }

    }
}
