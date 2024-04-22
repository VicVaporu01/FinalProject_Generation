using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

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
        // Lógica para activar o desactivar la cámara shake
        Cinemachine.CinemachineFreeLook[] freeLookCameras = FindObjectsOfType<Cinemachine.CinemachineFreeLook>();
        foreach (var camera in freeLookCameras)
        {
            camera.m_RecenterToTargetHeading.m_enabled = activar;
        }
    }
}
