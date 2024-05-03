using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseScreen;
    public GameObject pauseOverlay;
    public GameObject statsPanel;
    [SerializeField] private GameObject optionsMenuObject;
    [SerializeField] private GameObject resumeButtonObject;
    [SerializeField] private GameObject mapButtonObject;
    [SerializeField] private GameObject backOptionsMenuObject;
    [SerializeField] private CanvasGroup pauseMenuCanvasGroup;

    [Header("Stats Panel View")]
    public List<Image> speedSprites;
    public List<Image> damageSprites;
    public List<Image> maxLifeSprites;
    public List<Image> magicDamageSprites;
    public List<Image> bulletAmountSprites;

    [Header("Sprites Stats")]
    public Sprite spriteFull;
    public Sprite spriteHalf;
    public Sprite spriteEmpty;

    public bool isPaused = false;
    public static bool canPause = true;
    public int mainMenuIndex;

    void Start()
    {
        statsPanel.SetActive(false);
    }

    public void CloseMap()
    {
        if (isPaused && MapUIManager.Instance.isMapOpen)
        {
            MapUIManager.Instance.CloseMap();

            AudioManager.Instance.ClickBackwardsSound();

            EventSystem.current.SetSelectedGameObject(mapButtonObject);

            pauseMenuCanvasGroup.interactable = true;
        }
    }

    public void PauseUnpause()
    {
        if (canPause)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            pauseOverlay.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;

            if (isPaused)
            {
                MostrarEstadisticas();
                EventSystem.current.SetSelectedGameObject(resumeButtonObject);
                AudioManager.Instance.ClickBackwardsSound();
            }
            else
            {
                AudioManager.Instance.ClickForwardSound();
                statsPanel.SetActive(false);
            }
        }
    }

    private void TogglePlayerComponents(bool enable)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            Collider2D[] colliders = player.GetComponentsInChildren<Collider2D>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script != this)
                {
                    script.enabled = enable;
                }
            }

            foreach (Collider2D collider in colliders)
            {
                collider.enabled = enable;
            }
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManagerObject.Instance.LoadScene(mainMenuIndex);

        AudioManager.Instance.ClickBackwardsSound();
    }

    private void MostrarEstadisticas()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            UpdateStatSprites(speedSprites, playerStats.speedStats);
            UpdateStatSprites(damageSprites, playerStats.damangeStats);
            UpdateStatSprites(maxLifeSprites, playerStats.maxLifeStats);
            UpdateStatSprites(magicDamageSprites, playerStats.magicDamageStats); // Mostrar estad�stica de magicDamage
            UpdateStatSprites(bulletAmountSprites, playerStats.bulletAmountStats); // Mostrar estad�stica de bulletAmount

            statsPanel.SetActive(true);
        }
    }

    private void UpdateStatSprites(List<Image> statSprites, int statValue)
    {
        int fullSprites = statValue / 2;
        int halfSprite = statValue % 2;

        for (int i = 0; i < statSprites.Count; i++)
        {
            if (i < fullSprites)
            {
                statSprites[i].sprite = spriteFull;
            }
            else if (i == fullSprites && halfSprite == 1)
            {
                statSprites[i].sprite = spriteHalf;
            }
            else
            {
                statSprites[i].sprite = spriteEmpty; // Mostrar un sprite vac�o
            }
        }
    }

    public void OpenOptionsMenu()
    {
        pauseScreen.SetActive(false);

        optionsMenuObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(backOptionsMenuObject);

        AudioManager.Instance.ClickForwardSound();
    }

    public void OpenMap()
    {
        if (MapUIManager.Instance != null)
        {
            pauseMenuCanvasGroup.interactable = false;

            MapUIManager.Instance.OpenMap();

            AudioManager.Instance.ClickForwardSound();
        }
    }

}
