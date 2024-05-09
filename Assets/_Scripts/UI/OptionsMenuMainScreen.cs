using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenuMainScreen : MonoBehaviour
{
    [Header("Mute Sound Button")]
    [SerializeField] private Sprite mutedSoundSprite;
    [SerializeField] private Sprite unMutedSoundSprite;
    [SerializeField] private Image muteSoundImage;
    private bool isChangingMasterVolumeFirstTime = true;
    private bool isChangingMusicVolumeFirstTime = true;
    private bool isChangingSoundFXVolumeFirstTime = true;

    [Header("References")]
    [SerializeField] private GameObject optionsButtonGameObject;
    [SerializeField] private GameObject optionsMenuGameObject;
    [SerializeField] private GameObject mainMenuGameObject;

    [Header("Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;

    private void Start()
    {
        isChangingMasterVolumeFirstTime = true;
        isChangingMusicVolumeFirstTime = true;
        isChangingSoundFXVolumeFirstTime = true;

        CheckAudioState();

        InitializeSliders();
    }

    private void InitializeSliders()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_VOLUME_STRING, 0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_VOLUME_STRING, 0f);
        soundFXVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.SOUNDFX_VOLUME_STRING, 0f);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        if (!isChangingMasterVolumeFirstTime)
        {
            AudioManager.Instance.ClickForwardSound();
        }

        isChangingMasterVolumeFirstTime = false;

        AudioManager.Instance.ChangeMasterVolume(masterVolume);
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        if (!isChangingMusicVolumeFirstTime)
        {
            AudioManager.Instance.ClickForwardSound();
        }

        isChangingMusicVolumeFirstTime = false;

        AudioManager.Instance.ChangeMusicVolume(musicVolume);
    }

    public void ChangeSoundFXVolume(float soundFXVolume)
    {
        if (!isChangingSoundFXVolumeFirstTime)
        {
            AudioManager.Instance.ClickForwardSound();
        }

        isChangingSoundFXVolumeFirstTime = false;

        AudioManager.Instance.ChangeSoundEffectVolume(soundFXVolume);
    }

    public void MuteAudio()
    {
        AudioManager.Instance.ClickForwardSound();

        AudioManager.Instance.ChangeMuteState();

        CheckAudioState();
    }

    private void CheckAudioState()
    {
        if (AudioManager.Instance.isAudioMuted)
        {
            muteSoundImage.sprite = mutedSoundSprite;
        }
        else
        {
            muteSoundImage.sprite = unMutedSoundSprite;
        }
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.ClickBackwardsSound();

        optionsMenuGameObject.SetActive(false);

        mainMenuGameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(optionsButtonGameObject);
    }

}
