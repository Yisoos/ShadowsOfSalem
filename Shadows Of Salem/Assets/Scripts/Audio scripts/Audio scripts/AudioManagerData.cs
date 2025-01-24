using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerData", menuName = "Audio/AudioManagerData", order = 1)]
public class AudioManagerData : ScriptableObject
{
    public AudioClip background;
    public AudioClip clickButton;
    public AudioClip saltoDeEscritura;
    public AudioClip inicioButton;
    public AudioClip ajustesButton;
    public AudioClip salirButton;
    public AudioClip timbre;
}

