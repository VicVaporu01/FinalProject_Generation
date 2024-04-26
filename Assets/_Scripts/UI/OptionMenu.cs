using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public Toggle toggleCameraShake;

    private void Start()
    {
        // Asocia el m�todo ActivarDesactivarCameraShake al evento onValueChanged del Toggle
        toggleCameraShake.onValueChanged.AddListener(ActivarDesactivarCameraShake);
    }

    public void CambiarVolumenMusic(float volumen)
    {
        audioMixer.SetFloat("MusicVolume", volumen);
    }

    public void CambiarVolumenSFX(float volumen)
    {
        audioMixer.SetFloat("SFXVolume", volumen);
    }

    public void ActivarDesactivarCameraShake(bool activar)
    {
        // L�gica para activar o desactivar la c�mara shake
        Cinemachine.CinemachineFreeLook[] freeLookCameras = FindObjectsOfType<Cinemachine.CinemachineFreeLook>();
        foreach (var camera in freeLookCameras)
        {
            camera.m_RecenterToTargetHeading.m_enabled = activar;
        }
    }
}
