using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public List<Image> hearts; // Lista de corazones
    public Sprite heartFull, heartEmpty, heartHalf;
    public GameObject heartUIPrefab;
    public RectTransform heartContainer;
    private static UIHealthController instance;
    public static UIHealthController Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        int maxHealth = GameManager.Instance.GetMaxHealth();

        int currentHealth = GameManager.Instance.GetCurrentHealth();

        CreateHearths(maxHealth);

        UpdateHealthDisplay(currentHealth, maxHealth);
    }

    private void CreateHearths(int maxHealth)
    {
        float hearthAmount = MathF.Ceiling(maxHealth / 2f);

        for (int i = 0; i < hearthAmount; i++)
        {
            CreateHeart();
        }
    }

    private void CreateHeart()
    {
        GameObject hearthSpawned = Instantiate(heartUIPrefab, heartContainer);

        hearts.Add(hearthSpawned.GetComponent<Image>());
    }

    public void ChangeHearths(int newMaxHealth)
    {
        int expectedHearts = (int)Mathf.Ceil(newMaxHealth / 2f);
        int actualHeartAmount = hearts.Count;
        int heartsToChange = expectedHearts - actualHeartAmount;

        if (heartsToChange > 0)
        {
            for (int i = 0; i < heartsToChange; i++)
            {
                CreateHeart();
            }
        }
        else if (heartsToChange < 0)
        {
            for (int i = 0; i < MathF.Abs(heartsToChange); i++)
            {
                Destroy(hearts[^1].gameObject);
                hearts.RemoveAt(hearts.Count - 1);
            }
        }
    }

    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        // Calcular la cantidad de corazones completos y medios
        int fullHearts = currentHealth / 2;
        int halfHeart = currentHealth % 2; // Comprobar si hay medio coraz�n

        // Mostrar los corazones completos y medios
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < fullHearts)
            {
                hearts[i].sprite = heartFull;
            }
            else if (i == fullHearts && halfHeart == 1)
            {
                // Mostrar un coraz�n a la mitad si la vida es impar
                hearts[i].sprite = heartHalf;
            }
            else
            {
                // Mostrar un coraz�n vac�o
                hearts[i].sprite = heartEmpty;
            }

            // Activar o desactivar los corazones seg�n la cantidad m�xima de vida
            hearts[i].gameObject.SetActive(i < maxHealth / 2 + (maxHealth % 2));
        }
    }
}
