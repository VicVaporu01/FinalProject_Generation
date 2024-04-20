using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform objectRectTransform;
    [SerializeField] private UILineRenderer uILineRenderer;

    [Header("Parameters")]
    [SerializeField] private Color lineColor;
    private Vector3[] levelCorners = new Vector3[4];
    public List<MapLevel> PreviousLevels;
    public List<MapLevel> NextLevels;
    private Vector2 objectPositionVector2;

    private void Start()
    {
        objectRectTransform = GetComponent<RectTransform>();

        StartCoroutine(GetPositionCoroutine());
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

    public void ActivateNextLevels()
    {
        foreach (MapLevel mapLevel in NextLevels)
        {
            mapLevel.GetComponent<Button>().interactable = true;

            EventSystem.current.SetSelectedGameObject(mapLevel.gameObject);
        }

        GetComponent<Button>().interactable = false;
    }
}
