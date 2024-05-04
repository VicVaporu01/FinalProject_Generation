using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;

public class MenuGameOver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject menuGameOverObject;
    [SerializeField] private CanvasGroup backGroundObject;
    [SerializeField] private CanvasGroup youAreDeadImage;
    [SerializeField] private CanvasGroup mainMenuButtonObject;
    private PlayerHealthController playerDying;

    [Header("Animation Values")]
    [SerializeField] private float timeToOpenMenu;
    [SerializeField] private float timeToChangeBackgroundAlpha;
    [SerializeField] private float timeToChangeImageAlpha;
    [SerializeField] private float timeToChangeButtonAlpha;

    [Header("Sounds")]
    [SerializeField] private AudioClip gameOverSound;


    private void Start()
    {
        playerDying = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        playerDying.PlayerDead += MenuOn;
    }

    private void MenuOn(object sender, EventArgs e)
    {
        playerDying.PlayerDead -= MenuOn;

        PauseMenu.canPause = false;

        AudioManager.Instance.PlaySoundEffect(gameOverSound);

        AudioManager.Instance.StopMusic();

        StartCoroutine(StartGameOverMenuAnimation());
    }

    public void MainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);

        PauseMenu.canPause = true;

        AudioManager.Instance.ClickForwardSound();

        SceneManagerObject.Instance.LoadScene(0);
    }

    private IEnumerator StartGameOverMenuAnimation()
    {
        menuGameOverObject.SetActive(true);

        yield return new WaitForSeconds(timeToOpenMenu);

        LeanTween.alphaCanvas(backGroundObject, 1f, timeToChangeBackgroundAlpha).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(youAreDeadImage, 1f, timeToChangeImageAlpha).setOnComplete(() =>
            {
                LeanTween.alphaCanvas(mainMenuButtonObject, 1f, timeToChangeButtonAlpha).setOnComplete(() =>
                {
                    EventSystem.current.SetSelectedGameObject(mainMenuButtonObject.gameObject);
                });
            });
        }
        );
    }
}
