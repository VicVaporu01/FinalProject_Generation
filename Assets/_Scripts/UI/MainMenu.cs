using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject optionsMenuObject;
    [SerializeField] private GameObject optionsMenuButtonObject;
    [SerializeField] private GameObject backMenuObject;
    [SerializeField] private GameObject playButtonObject;

    [Header("How To Play")]
    [SerializeField] private GameObject returnHowToPlayButton;
    [SerializeField] private GameObject howToPlayButton;
    [SerializeField] private GameObject howToPlayPanel;

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

        EventSystem.current.SetSelectedGameObject(playButtonObject);

        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameScene()
    {
        SceneManagerObject.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenOptionsMenu()
    {
        mainMenuObject.SetActive(false);

        optionsMenuObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(backMenuObject);
    }

    public void ExitOptionsMenu()
    {
        mainMenuObject.SetActive(true);

        optionsMenuObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(optionsMenuButtonObject);
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }

    public void OpenHowToPlayPanel()
    {
        mainMenuObject.SetActive(false);

        howToPlayPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(returnHowToPlayButton);
    }

    public void ReturnToMainMenuFromHowToPlay()
    {
        mainMenuObject.SetActive(true);

        howToPlayPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(howToPlayButton);
    }
}
