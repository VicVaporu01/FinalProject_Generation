using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public List<Image> hearts; // Lista de corazones
    public Sprite heartFull, heartEmpty, heartHalf;
    
    private static UIHealthController instance;


    public static UIHealthController Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        // Actualizar la IU con la vida maxima del jugador al inicio del juego
        int maxHealth = GameManager.Instance.GetMaxHealth();
        UpdateHealthDisplay(maxHealth, maxHealth);
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
