using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);   
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public MapLevelTypeEnum GiveDifficultyToLevel()
    {
        return MapUIManager.Instance.actualMapLevel.mapLevelType;
    }
    
}
