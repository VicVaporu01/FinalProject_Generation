using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private PlayerMeleeAttack playerAttack;

    /*private void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMeleeAttack>();
        playerAttack.PlayerDead += MenuOn;
    }
    */
    private void MenuOn(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
