using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Audio/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    private static SoundDatabase _instance;

    public static SoundDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<SoundDatabase>("SoundDatabase");
                if (_instance == null)
                {
                    Debug.LogError("No se encontró el SoundDatabase en Resources.");
                }
            }
            return _instance;
        }
    }

    [Header("Sound Effects")]
    public Sound[] soundEffects; 

    [Header("Background Music")]
    public Sound[] backgroundMusic; 

    [Header("UI Sounds")]
    public Sound[] uiSounds; 

    public AudioClip GetSound(string name, SoundType type)
    {
        Sound[] soundsToSearch = type switch
        {
            SoundType.SFX => soundEffects,
            SoundType.BGMUSIC => backgroundMusic,
            SoundType.UI => uiSounds,
            _ => null
        };

        if (soundsToSearch != null)
        {
            foreach (var sound in soundsToSearch)
            {
                if (sound.name == name) return sound.clip;
            }
        }

        Debug.LogWarning("Sonido no encontrado: " + name);
        return null;
    }
}

public enum SoundType
{
    SFX,
    BGMUSIC,
    UI 
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
