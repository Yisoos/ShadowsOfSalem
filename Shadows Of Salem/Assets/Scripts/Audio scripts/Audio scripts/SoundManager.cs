using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public SoundDatabase soundDatabase;

    [Range(0f, 1f)]
    public float SFXVolume = 1f; // Volume for SFX
    [Range(0f, 1f)]
    public float BGMVolume = 1f; // Volume for BGM
    [Range(0f, 1f)]
    public float UIVolume = 1f; // Volume for UI sounds

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string soundName, SoundType type)
    {
        AudioClip clip = soundDatabase.GetSound(soundName, type);
        if (clip != null)
        {
            switch (type)
            {
                case SoundType.SFX:
                    AudioSource sfxSource = GetComponent<AudioSource>();
                    sfxSource.PlayOneShot(clip, SFXVolume);
                    break;
                    
                case SoundType.BGM:
                    PlayBackgroundMusic(clip);
                    break;

                case SoundType.UI:
                    PlayUISound(clip);
                    break;
                    
            }
        }
    }
    
    public void PlayBackgroundMusic(AudioClip clip)
    {
        AudioSource bgmSource = GetComponent<AudioSource>();
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.volume = BGMVolume;
        bgmSource.Play();
    }

    public void PlayUISound(AudioClip clip)
    {
        GameObject tempObject = new GameObject("TempUISound");
        AudioSource tempSource = tempObject.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = UIVolume;
        tempSource.Play();
        Destroy(tempObject, clip.length); // limpiar
    }
}
