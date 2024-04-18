using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform contentRectTransform;
    private GameObject selectedGameObject;
    private RectTransform selectedGameObjectRectTransform;

    [Header("Animation Values")]
    [SerializeField] private float stepValue;
    [SerializeField] private float timeToComplete;

    private void Update()
    {
        if (selectedGameObject != EventSystem.current.currentSelectedGameObject)
        {
            selectedGameObject = EventSystem.current.currentSelectedGameObject;

            if (selectedGameObject != null)
            {
                selectedGameObjectRectTransform = selectedGameObject.GetComponent<RectTransform>();
            }
        }

        if (selectedGameObject != null)
        {
            if (!IsFullyVisibleFrom(selectedGameObjectRectTransform))
            {
                if (IsObjectInRightBound(selectedGameObjectRectTransform))
                {
                    LeanTween.moveX(contentRectTransform, contentRectTransform.anchoredPosition.x - stepValue, timeToComplete);
                }
                else
                {
                    LeanTween.moveX(contentRectTransform, contentRectTransform.anchoredPosition.x + stepValue, timeToComplete);
                }
            }
        }
    }

    private bool IsObjectInRightBound(RectTransform rectTransform)
    {
        Vector3[] objectCorners = new Vector3[4];

        rectTransform.GetWorldCorners(objectCorners);

        foreach (var item in objectCorners)
        {
            if (item.x < 0)
            {
                return false;
            }
        }

        return true;
    }
    private int CountCornersVisibleFrom(RectTransform rectTransform)
    {
        Rect screenBounds = new(0f, 0f, Screen.width, Screen.height);
        Vector3[] objectCorners = new Vector3[4];

        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        for (var i = 0; i < objectCorners.Length; i++)
        {
            if (screenBounds.Contains(objectCorners[i]))
            {
                visibleCorners++;
            }
        }
        return visibleCorners;
    }

    private bool IsFullyVisibleFrom(RectTransform rectTransform)
    {
        return CountCornersVisibleFrom(rectTransform) == 4;
    }

    private bool IsVisibleFrom(RectTransform rectTransform)
    {
        return CountCornersVisibleFrom(rectTransform) > 0;
    }

}
