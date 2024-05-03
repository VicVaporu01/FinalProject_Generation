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
    public const string MASTER_VOLUME_STRING = "MasterVolume";
    public const string MUSIC_VOLUME_STRING = "MusicVolume";
    public const string SOUNDFX_VOLUME_STRING = "SoundFXVolume";

    [Header("Music Volume Changes")]
    [SerializeField] private float timeToChangeMusic;
    [SerializeField] private float waitTimeToChangeMusicClip;
    public bool isAudioMuted = false;

    [Header("Sounds")]
    [SerializeField] private AudioClip clickForward;
    [SerializeField] private AudioClip clickBackwards;
    [SerializeField] private AudioClip enterLevelSound;
    [SerializeField] private AudioClip spawnObjectsSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
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

    public void ChangeMuteState()
    {
        isAudioMuted = !isAudioMuted;

        musicAudioSource.mute = isAudioMuted;

        soundFXAudioSource.mute = isAudioMuted;
    }

    public void ClickForwardSound()
    {
        PlaySoundEffect(clickForward);
    }

    public void ClickBackwardsSound()
    {
        PlaySoundEffect(clickBackwards);
    }

    public void EnterLevelSound()
    {
        PlaySoundEffect(enterLevelSound);
    }

    public void SpawnObjectsSound()
    {
        PlaySoundEffect(spawnObjectsSound);
    }

}
