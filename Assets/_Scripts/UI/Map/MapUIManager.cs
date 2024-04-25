using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapUIManager : MonoBehaviour
{
    public static MapUIManager Instance;

    [Header("References")]
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private CanvasGroup mapCanvasGroup;
    [SerializeField] private GameObject selectedGameObject;
    private RectTransform selectedGameObjectRectTransform;

    [Header("Animation Values")]
    [SerializeField] private float stepValue;
    [SerializeField] private float timeToComplete;
    [SerializeField] private float timeToOpenMap;
    [SerializeField] private float timeToCloseMap;
    [SerializeField] private LeanTweenType easeOpenMap;
    [SerializeField] private LeanTweenType easeCloseMap;

    [Header("Map Controll Values")]
    [SerializeField] private List<MapStage> mapStagesSpawned;
    [SerializeField] public MapLevel actualMapLevel;
    [SerializeField] private int actualMapStageIndex = 0;
    [SerializeField] private bool isPlayingALevel = false;
    private bool isMapOpen = true;

    [Header("Level Control")]
    [SerializeField] private int initialLevelIndex;
    [SerializeField] private int finalLevelIndex;
    public int[] playableLevelsIndexList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageCompleted();
        }

        if (isMapOpen)
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

    public void AddSpawnedStage(MapStage mapStage)
    {
        mapStagesSpawned.Add(mapStage);
    }

    public void StageCompleted()
    {
        if (isPlayingALevel)
        {
            isPlayingALevel = false;

            mapStagesSpawned[actualMapStageIndex].DisableAllMapLevelsInStage();

            actualMapLevel.ActivateNextLevelsInMap();

            actualMapStageIndex++;

            Invoke(nameof(OpenMap), 1.5f);
        }
    }

    public void SetActualMapLevel(MapLevel mapLevel)
    {
        actualMapLevel = mapLevel;
    }

    public void SetActualMapLevelToPlay(MapLevel mapLevel)
    {
        if (!isPlayingALevel)
        {
            isPlayingALevel = true;

            actualMapLevel = mapLevel;

            mapStagesSpawned[actualMapStageIndex].DisableAllMapLevelsInStageExcept(actualMapLevel);

            SceneManagerObject.Instance.LoadScene(mapLevel.GetLevelMapIndex());

            Invoke(nameof(CloseMap), 0.5f);
        }
    }

    public MapLevel GetActualMapLevelToPlay()
    {
        return actualMapLevel;
    }

    public int GetRandomLevelToPlay()
    {
        return playableLevelsIndexList[Random.Range(0, playableLevelsIndexList.Length)];
    }

    public int GetInitialLevelIndex()
    {
        return initialLevelIndex;
    }

    public int GetFinalLevelIndex()
    {
        return finalLevelIndex;
    }

    [ContextMenu("Open Map")]
    public void OpenMap()
    {
        LeanTween.alphaCanvas(mapCanvasGroup, 1f, timeToOpenMap).setEase(easeOpenMap).setOnComplete(() =>
        {
            mapCanvasGroup.blocksRaycasts = true;
            isMapOpen = true;
            EventSystem.current.SetSelectedGameObject(actualMapLevel.gameObject);
        });
    }

    [ContextMenu("Close Map")]
    public void CloseMap()
    {
        isMapOpen = false;

        LeanTween.alphaCanvas(mapCanvasGroup, 0f, timeToCloseMap).setEase(easeCloseMap).setOnComplete(() => mapCanvasGroup.blocksRaycasts = false);
    }
}
