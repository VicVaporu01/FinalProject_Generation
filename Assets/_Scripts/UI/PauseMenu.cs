using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string mainMenu;

    public GameObject pauseScreen;
    public GameObject pauseOverlay;
    public GameObject statsPanel;

    public List<Image> speedSprites; // Lista de sprites para la velocidad
    public List<Image> damageSprites; // Lista de sprites para el daño
    public List<Image> maxLifeSprites; // Lista de sprites para la vida máxima

    public Sprite spriteFull; // Sprite para representar una estadística completa
    public Sprite spriteHalf; // Sprite para representar una estadística a la mitad

    public bool isPaused;

    private string currentScene;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        statsPanel.SetActive(false); // Ocultar el panel de estadísticas al inicio
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);
        pauseOverlay.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        TogglePlayerComponents(!isPaused);

        if (isPaused)
        {
            MostrarEstadisticas(); // Mostrar las estadísticas al pausar
        }
        else
        {
            statsPanel.SetActive(false); // Ocultar el panel de estadísticas al despausar
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    private void MostrarEstadisticas()
    {
        // Obtener el componente PlayerStats
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            // Mostrar las estadísticas en el panel de estadísticas
            UpdateStatSprites(speedSprites, playerStats.speedStats);
            UpdateStatSprites(damageSprites, playerStats.damangeStats);
            UpdateStatSprites(maxLifeSprites, playerStats.maxLifeStats);

            statsPanel.SetActive(true); // Mostrar el panel de estadísticas
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
                statSprites[i].sprite = null; // Sprite vacío
            }
        }
    }
}
