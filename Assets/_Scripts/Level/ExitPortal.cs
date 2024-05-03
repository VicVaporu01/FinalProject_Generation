using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] private GameObject activatePortalEffect;
    [SerializeField] private AudioClip enterPortalSound;

    public void ExitLevel()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;

            playerMovement.StopPlayerMovement();

            Instantiate(activatePortalEffect, transform.position, Quaternion.identity);

            AudioManager.Instance.PlaySoundEffect(enterPortalSound);
        }

        MapUIManager.Instance.StageCompleted();
    }

}
