using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [Header("Mute Sound Button")]
    [SerializeField] private Sprite mutedSoundSprite;
    [SerializeField] private Sprite unMutedSoundSprite;
    [SerializeField] private Image muteSoundImage;

    [Header("References")]
    [SerializeField] private GameObject optionsButtonGameObject;
    [SerializeField] private GameObject optionsMenuGameObject;
    [SerializeField] private GameObject pauseMenuGameObject;

    private void Start()
    {
        CheckAudioState();
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        AudioManager.Instance.ChangeMasterVolume(masterVolume);
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        AudioManager.Instance.ChangeMusicVolume(musicVolume);
    }

    public void ChangeSoundFXVolume(float soundFXVolume)
    {
        AudioManager.Instance.ChangeSoundEffectVolume(soundFXVolume);
    }

    public void MuteAudio()
    {
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

    public void BackToPauseMenuButton()
    {
        optionsMenuGameObject.SetActive(false);

        pauseMenuGameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(optionsButtonGameObject);
    }

}
