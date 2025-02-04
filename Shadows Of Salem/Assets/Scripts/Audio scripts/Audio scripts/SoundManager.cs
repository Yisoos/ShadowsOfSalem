using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Range(0f, 1f)]
    public float sfxVolume = 1f; // Volume for SFX
    [Range(0f, 1f)]
    public float bgmVolume = 1f; // Volume for BGM
    [Range(0f, 1f)]
    public float uiVolume = 1f; // Volume for UI sounds

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
        AudioClip clip = SoundDatabase.Instance.GetSound(soundName, type);
        if (clip != null)
        {
            switch (type)
            {
                case SoundType.SFX:
                    AudioSource sfxSource = GetComponent<AudioSource>();
                    sfxSource.PlayOneShot(clip, sfxVolume);
                    break;
                    
                case SoundType.BGMUSIC:
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
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void PlayUISound(AudioClip clip)
    {
        GameObject tempObject = new GameObject("TempUISound");
        AudioSource tempSource = tempObject.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = uiVolume;
        tempSource.Play();
        Destroy(tempObject, clip.length); // limpiar
    }
}
