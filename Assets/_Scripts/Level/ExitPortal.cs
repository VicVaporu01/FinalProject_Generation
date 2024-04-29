using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] private ParticleSystem hoverIndicator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hoverIndicator.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hoverIndicator.Stop();
        }
    }

    public void ExitLevel()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        MapUIManager.Instance.StageCompleted();
    }

}
