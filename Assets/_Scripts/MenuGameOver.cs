using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private PlayerHealthController playerDying;
    private SceneManagerObject sceneManager;

    private void Start()
    {
        playerDying = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        playerDying.PlayerDead += MenuOn;
        sceneManager = GameObject.FindObjectOfType<SceneManagerObject>();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void MenuOn(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
        
    }
    

    public void MainMenu(string nombre)
    {
        SceneManagerObject.Instance.LoadScene(0);
    }

    public void Exit()
    {
        sceneManager.LoadNextScene();
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
