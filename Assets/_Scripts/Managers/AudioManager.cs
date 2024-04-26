using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("References")]
    [SerializeField] private AudioSource soundFXAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioMixer masterAudioMixer;

    [Header("Strings Values")]
    private const string MASTER_VOLUME_STRING = "MasterVolume";
    private const string MUSIC_VOLUME_STRING = "MusicVolume";
    private const string SOUNDFX_VOLUME_STRING = "SoundFXVolume";

    [Header("Music Volume Changes")]
    [SerializeField] private float timeToChangeMusic;
    [SerializeField] private float waitTimeToChangeMusicClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetInitialSavedVolume();
    }

    private void SetInitialSavedVolume()
    {
        masterAudioMixer.SetFloat(MASTER_VOLUME_STRING, PlayerPrefs.GetFloat(MASTER_VOLUME_STRING, 0f));
        masterAudioMixer.SetFloat(MUSIC_VOLUME_STRING, PlayerPrefs.GetFloat(MUSIC_VOLUME_STRING, 0f));
        masterAudioMixer.SetFloat(SOUNDFX_VOLUME_STRING, PlayerPrefs.GetFloat(SOUNDFX_VOLUME_STRING, 0f));
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        soundFXAudioSource.PlayOneShot(audioClip);
    }

    public void ChangeSoundEffectVolume(float soundEffectVolume)
    {
        masterAudioMixer.SetFloat(SOUNDFX_VOLUME_STRING, soundEffectVolume);

        PlayerPrefs.SetFloat(SOUNDFX_VOLUME_STRING, soundEffectVolume);
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        masterAudioMixer.SetFloat(MUSIC_VOLUME_STRING, musicVolume);

        PlayerPrefs.SetFloat(MUSIC_VOLUME_STRING, musicVolume);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        masterAudioMixer.SetFloat(MASTER_VOLUME_STRING, masterVolume);

        PlayerPrefs.SetFloat(MASTER_VOLUME_STRING, masterVolume);
    }

    [ContextMenu("Stop Music")]
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicAudioSource.Stop();

        musicAudioSource.clip = musicClip;

        musicAudioSource.Play();
    }

    public void ChangeMusic(AudioClip musicClip)
    {
        float currentVolume = musicAudioSource.volume;

        LeanTween.value(gameObject, currentVolume, 0f, timeToChangeMusic)
            .setOnUpdate((float value) =>
            {
                musicAudioSource.volume = value;
            }).setOnComplete(() =>
            {
                StartCoroutine(ChangeAudioClipWaitTime(musicClip));
            });
    }

    private IEnumerator ChangeAudioClipWaitTime(AudioClip musicClip)
    {
        yield return new WaitForSeconds(waitTimeToChangeMusicClip);

        float currentVolume = musicAudioSource.volume;

        musicAudioSource.clip = musicClip;

        musicAudioSource.Play();

        LeanTween.value(gameObject, currentVolume, 1f, timeToChangeMusic)
            .setOnUpdate((float value) =>
            {
                musicAudioSource.volume = value;
            });
    }

}
