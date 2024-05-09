using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup destinationMapPanelCanvasGroup;
    [SerializeField] private GameObject victoryPanelObject;
    [SerializeField] private CanvasGroup backgroundCanvasGroup;
    [SerializeField] private CanvasGroup victoryTextCanvasGroup;
    [SerializeField] private CanvasGroup tittleCanvasGroup;
    [SerializeField] private CanvasGroup mainMenuButtonCanvasGroup;

    [Header("Timers")]
    [SerializeField] private float timeToStartAnimations;
    [SerializeField] private float timeToCloseMap;
    [SerializeField] private float timeToOpenVictoryPanel;
    [SerializeField] private float timeToSetVictoryText;
    [SerializeField] private float timeToSetTittle;
    [SerializeField] private float timeToSetButton;

    [Header("Easings")]
    [SerializeField] private LeanTweenType victoryTextEase;
    [SerializeField] private LeanTweenType tittleEase;
    [SerializeField] private LeanTweenType buttonEase;


    public void StartVictoryPanel()
    {
        StartCoroutine(StartVictoryPanelCoroutine());
    }

    public IEnumerator StartVictoryPanelCoroutine()
    {
        yield return new WaitForSeconds(timeToStartAnimations);

        EventSystem.current.SetSelectedGameObject(null);

        destinationMapPanelCanvasGroup.interactable = false;

        destinationMapPanelCanvasGroup.blocksRaycasts = false;

        LeanTween.alphaCanvas(destinationMapPanelCanvasGroup, 0f, timeToCloseMap).setOnComplete(() =>
        {
            victoryPanelObject.SetActive(true);

            LeanTween.alphaCanvas(backgroundCanvasGroup, 1f, timeToOpenVictoryPanel).setOnComplete(() =>
            {
                LeanTween.alphaCanvas(victoryTextCanvasGroup, 1f, timeToSetVictoryText).setEase(victoryTextEase).setOnComplete(() =>
                {
                    LeanTween.alphaCanvas(tittleCanvasGroup, 1f, timeToSetTittle).setEase(tittleEase).setOnComplete(() =>
                    {
                        LeanTween.alphaCanvas(mainMenuButtonCanvasGroup, 1f, timeToSetButton).setEase(buttonEase).setOnComplete(() =>
                        {
                            mainMenuButtonCanvasGroup.interactable = true;

                            mainMenuButtonCanvasGroup.blocksRaycasts = true;

                            EventSystem.current.SetSelectedGameObject(mainMenuButtonCanvasGroup.gameObject);
                        });
                    });
                });
            });
        });
    }

    public void BackToMainMenuVictory()
    {
        EventSystem.current.SetSelectedGameObject(null);

        SceneManagerObject.Instance.LoadScene(0);
    }

}
