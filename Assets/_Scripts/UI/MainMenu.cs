using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
    
    public void ChangeScene(string Escena)
    {
        SceneManager.LoadScene(Escena);
    }
    
}
