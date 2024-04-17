using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public static UIHealthController instance;

    public List<Image> hearts; // Lista de corazones
    public Sprite heartFull, heartEmpty, heartHalf;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Actualizar la IU con la vida máxima del jugador al inicio del juego
        int maxHealth = PlayerHealthController.instance.maxHealth;
        UpdateHealthDisplay(maxHealth, maxHealth);
    }

    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        // Calcular la cantidad de corazones completos y medios
        int fullHearts = currentHealth / 2;
        int halfHeart = currentHealth % 2; // Comprobar si hay medio corazón

        // Mostrar los corazones completos y medios
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < fullHearts)
            {
                hearts[i].sprite = heartFull;
            }
            else if (i == fullHearts && halfHeart == 1)
            {
                // Mostrar un corazón a la mitad si la vida es impar
                hearts[i].sprite = heartHalf;
            }
            else
            {
                // Mostrar un corazón vacío
                hearts[i].sprite = heartEmpty;
            }

            // Activar o desactivar los corazones según la cantidad máxima de vida
            hearts[i].gameObject.SetActive(i < maxHealth / 2 + (maxHealth % 2));
        }
    }
}
