using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string mainMenu;

    public GameObject pauseScreen;
    public GameObject pauseOverlay;
    public GameObject statsPanel;
    public TMP_Text speedText;
    public TMP_Text damageText;
    public TMP_Text maxLifeText;

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
            speedText.text = "Speed: " + playerStats.speedStats;
            damageText.text = "Damage: " + playerStats.damangeStats;
            maxLifeText.text = "Max Life: " + playerStats.maxLifeStats;

            statsPanel.SetActive(true); // Mostrar el panel de estadísticas
        }
    }
}
