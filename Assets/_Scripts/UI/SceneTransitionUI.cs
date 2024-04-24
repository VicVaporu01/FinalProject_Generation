using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    public static SceneTransitionUI Instance;

    [Header("Fade Effect")]
    [SerializeField] private CanvasGroup fadeEffect;
    [SerializeField] private float timeFade;

    [Header("Cross Effect")]
    [SerializeField] private GameObject crossEffectObject;
    [SerializeField] private CanvasGroup crossEffectCanvasGroup;
    [SerializeField] private float timeToCloseCross;
    [SerializeField] private RectTransform topObject;
    [SerializeField] private RectTransform botObject;
    [SerializeField] private LeanTweenType crossEaseType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EnterSceneFade();
    }

    public void EnterSceneFade()
    {
        LeanTween.alphaCanvas(fadeEffect, 0, timeFade).setEase(LeanTweenType.easeInCirc).setOnComplete(DisableInteractableObjects);
    }

    [ContextMenu("ExitScene")]
    public float ExitSceneCross()
    {
        crossEffectObject.SetActive(true);

        LeanTween.moveY(topObject, 0f, timeToCloseCross).setEase(crossEaseType);

        LeanTween.moveY(botObject, 0f, timeToCloseCross).setEase(crossEaseType);

        return timeToCloseCross;
    }


    private void DisableInteractableObjects()
    {
        fadeEffect.interactable = false;
        fadeEffect.blocksRaycasts = false;
    }

    private void EnableInteractableObjects()
    {
        fadeEffect.interactable = true;
        fadeEffect.blocksRaycasts = true;
    }
}
