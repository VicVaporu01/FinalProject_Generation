using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] private GameObject activatePortalEffect;
    [SerializeField] private AudioClip enterPortalSound;
    private bool canExitLevel;

    private void Start()
    {
        canExitLevel = true;
    }

    public void ExitLevel()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null && canExitLevel)
        {
            playerMovement.StopPlayerMovement();

            Instantiate(activatePortalEffect, transform.position, Quaternion.identity);

            AudioManager.Instance.PlaySoundEffect(enterPortalSound);

            canExitLevel = false;

            MapUIManager.Instance.StageCompleted();
        }
    }
}
