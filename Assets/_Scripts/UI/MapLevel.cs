using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private RectTransform objectRectTransform;
    [SerializeField] private UILineRenderer uILineRenderer;
    [SerializeField] private MapLevel[] testObjectives;
    [SerializeField] public Vector2 objectPositionVector2;
    [SerializeField] private Vector3[] objectCornersObject = new Vector3[4];

    [SerializeField] private List<MapLevel> previousLevels;
    [SerializeField] private List<MapLevel> nextLevels;
    [SerializeField] private float multiplyFactor;
    [SerializeField] private Color lineColor;

    private void Start()
    {
        multiplyFactor = 3f;

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

        objectCornersObject = objectCornersInternal;

        objectPositionVector2 = new Vector2((objectCornersObject[1].x + objectCornersObject[2].x) / 2, (objectCornersObject[1].y + objectCornersObject[0].y) / 2);
    }

    public void GenerateLines()
    {
        MapLevel[] objectives = previousLevels.ToArray();

        Vector2[] lines = new Vector2[objectives.Length * 2];

        for (int i = 0; i < lines.Length; i++)
        {
            if (i % 2 == 0)
            {
                lines[i] = new Vector2(0, 0);
            }
            else
            {
                Vector2 objetivePosition = new((objectives[i / 2].objectPositionVector2.x - objectPositionVector2.x) * multiplyFactor, (objectives[i / 2].objectPositionVector2.y - objectPositionVector2.y) * multiplyFactor);

                lines[i] = objetivePosition;
            }
        }

        uILineRenderer.points = lines;

        uILineRenderer.color = lineColor;
    }

    public void AddPreviousLevel(MapLevel levelGameObject)
    {
        previousLevels.Add(levelGameObject);
    }

    public void AddNextLevel(MapLevel levelGameObject)
    {
        nextLevels.Add(levelGameObject);
    }

    public void LoadInitialScene()
    {
        SceneManager.LoadScene(1);
    }
}
