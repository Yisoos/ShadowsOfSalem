using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Audio/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    //MUSIC
    public Sound[] backgroundMusic;

    [Space(10)]
    public Sound[] soundEffects;

    [Space(10)]
    public Sound[] uiSounds; 

    public AudioClip GetSound(string name, SoundType type)
    {
        Sound[] soundsToSearch = type switch
        {
            SoundType.SFX => soundEffects,
            SoundType.BGM => backgroundMusic,
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
    BGM,
    UI 
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
