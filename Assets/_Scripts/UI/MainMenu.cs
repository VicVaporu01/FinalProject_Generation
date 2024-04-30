using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (MapUIManager.Instance != null)
        {
            Destroy(MapUIManager.Instance.gameObject);
        }

        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
    }

    public void GameScene()
    {
        SceneManagerObject.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
}
