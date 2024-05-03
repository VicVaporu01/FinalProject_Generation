using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    public void ExitLevel()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;

            playerMovement.StopPlayerMovement();
        }

        MapUIManager.Instance.StageCompleted();
    }

}
