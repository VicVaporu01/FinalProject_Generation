using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string mainMenu;

    public GameObject pauseScreen;
    public GameObject pauseOverlay; 
    public bool isPaused;

    private string currentScene;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        currentScene = SceneManager.GetActiveScene().name;
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
}
