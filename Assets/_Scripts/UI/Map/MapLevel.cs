using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform objectRectTransform;
    [SerializeField] private UILineRenderer uILineRenderer;
    [SerializeField] private Button mapButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;

    [Header("Parameters")]
    [SerializeField] private Color lineColor;
    [SerializeField] private bool canSelectLevel = false;
    [SerializeField] private int levelToLoadIndex;
    public MapLevelTypeEnum mapLevelType;
    public List<MapLevel> PreviousLevels;
    public List<MapLevel> NextLevels;
    private Vector3[] levelCorners = new Vector3[4];
    private Vector2 objectPositionVector2;

    [Header("Image Values")]
    [Range(0, 1)][SerializeField] private float hueDownValue;

    private void Start()
    {
        objectRectTransform = GetComponent<RectTransform>();

        mapButton.onClick.AddListener(SelectStage);

        SetLevelMapIndex();

        StartCoroutine(GetPositionCoroutine());
    }

    private void SetLevelMapIndex()
    {
        switch (mapLevelType)
        {
            case MapLevelTypeEnum.InitialLevel:
                levelToLoadIndex = MapUIManager.Instance.GetInitialLevelIndex();
                break;
            case MapLevelTypeEnum.BossLevel:
                levelToLoadIndex = MapUIManager.Instance.GetFinalLevelIndex();
                break;
            case MapLevelTypeEnum.NormalLevel:
                levelToLoadIndex = MapUIManager.Instance.GetRandomLevelToPlay();
                break;
            case MapLevelTypeEnum.HardLevel:
                levelToLoadIndex = MapUIManager.Instance.GetRandomLevelToPlay();
                break;
            case MapLevelTypeEnum.ShopLevel:
                levelToLoadIndex = MapUIManager.Instance.GetRandomLevelToPlay();
                break;
        }
    }

    public int GetLevelMapIndex()
    {
        return levelToLoadIndex;
    }

    private void SelectStage()
    {
        if (canSelectLevel && !MapUIManager.Instance.isPlayingALevel)
        {
            MapUIManager.Instance.SetActualMapLevelToPlay(this);

            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private IEnumerator GetPositionCoroutine()
    {
        yield return new WaitForEndOfFrame();

        GetPosition(objectRectTransform);
    }

    private void GetPosition(RectTransform rectTransform)
    {
        Vector3[] objectCornersInternal = new Vector3[4];

        rectTransform.GetWorldCorners(objectCornersInternal);

        levelCorners = objectCornersInternal;

        objectPositionVector2 = new Vector2((levelCorners[1].x + levelCorners[2].x) / 2, (levelCorners[1].y + levelCorners[0].y) / 2);
    }

    public void GenerateLines(float multiplyFactor)
    {
        MapLevel[] objectives = NextLevels.ToArray();

        Vector2[] lines = new Vector2[objectives.Length * 2];

        for (int i = 0; i < lines.Length; i++)
        {
            if (i % 2 == 0)
            {
                lines[i] = new Vector2(0, 0);
            }
            else
            {
                Vector2 objetivePosition = new(
                    (objectives[i / 2].objectPositionVector2.x - objectPositionVector2.x) * multiplyFactor,
                    (objectives[i / 2].objectPositionVector2.y - objectPositionVector2.y) * multiplyFactor);

                lines[i] = objetivePosition;
            }
        }

        uILineRenderer.points = lines;

        uILineRenderer.color = lineColor;
    }

    public void AddPreviousLevel(MapLevel levelGameObject)
    {
        PreviousLevels.Add(levelGameObject);
    }

    public void AddNextLevel(MapLevel levelGameObject)
    {
        NextLevels.Add(levelGameObject);
    }

    public int ActivateNextLevelsInMap()
    {
        int activatedLevelsAmount = 0;

        foreach (MapLevel mapLevel in NextLevels)
        {
            mapLevel.ActivateLevel();

            EventSystem.current.SetSelectedGameObject(mapLevel.gameObject);

            activatedLevelsAmount++;
        }

        canSelectLevel = false;

        return activatedLevelsAmount;
    }

    public void DisableMapLevel()
    {
        canSelectLevel = false;

        backgroundImage.color = new Color(backgroundImage.color.r * hueDownValue, backgroundImage.color.g * hueDownValue, backgroundImage.color.b * hueDownValue, 1f);

        iconImage.color = new Color(iconImage.color.r * hueDownValue, iconImage.color.g * hueDownValue, iconImage.color.b * hueDownValue, 1f);
    }

    public void ActivateLevel()
    {
        canSelectLevel = true;

        backgroundImage.color = new Color(backgroundImage.color.r / hueDownValue, backgroundImage.color.g / hueDownValue, backgroundImage.color.b / hueDownValue, 1f);

        iconImage.color = new Color(iconImage.color.r / hueDownValue, iconImage.color.g / hueDownValue, iconImage.color.b / hueDownValue, 1f);
    }

    public void ChangeBackgroundSprite(Sprite newBackgroundSprite)
    {
        backgroundImage.sprite = newBackgroundSprite;
    }
}
