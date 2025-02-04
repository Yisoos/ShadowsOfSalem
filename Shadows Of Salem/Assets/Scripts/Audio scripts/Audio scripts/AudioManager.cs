using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Datos de Audio")]
    public AudioManagerData audioData; // El ScriptableObject donde se almacenan los clips

    [Header("Fuentes de Audio")]
    [SerializeField] private AudioSource musicSource;  // Fuente de m�sica
    [SerializeField] private AudioSource SFXSource;    // Fuente de efectos de sonido

    [Header("Volumen")]
    [Range(0f, 1f)] public float volumeSFX = 1f; // Volumen de efectos de sonido
    [Range(0f, 1f)] public float volumeMusic = 1f; // Volumen de m�sica de fondo


    private void Awake()
    {
        // Implementaci�n del patr�n Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina el AudioManager duplicado
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }

        // Establecer vol�menes al iniciar
        SetVolumeSFX(volumeSFX);
        SetVolumeMusic(volumeMusic);
    }

    private void Start()
    {
        // Asignar y reproducir m�sica de fondo si se ha configurado audioData
        if (audioData != null && musicSource != null)
        {
            musicSource.clip = audioData.backgroundINTRO;
            musicSource.Play();
        }
    }

    // M�todo para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip); // Reproducir el efecto de sonido
        }
    }

    // M�todo para pausar el audio (cuando se cambia de escena, por ejemplo)
    public void PauseAudio()
    {
        AudioListener.pause = true;
        musicSource.Pause(); // Pausa la m�sica de fondo si est� sonando
    }

    // M�todo para reanudar el audio
    public void ResumeAudio()
    {
        AudioListener.pause = false;
        musicSource.Play(); // Reanuda la m�sica de fondo si estaba pausada
    }

    public void StopPlayingAudio() 
    {
        Debug.Log("stopping");

        SFXSource.Stop();
        musicSource.Stop();
    }

    // M�todo para ajustar el volumen de los efectos de sonido
    public void SetVolumeSFX(float volume)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    // M�todo para ajustar el volumen de la m�sica de fondo
    public void SetVolumeMusic(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    // Cambia la m�sica de fondo entre escenas
    public void ChangeBackgroundMusic(AudioClip newBackgroundClip)
    {
        if (musicSource != null && newBackgroundClip != null)
        {
            musicSource.clip = newBackgroundClip;
            musicSource.Play();
        }
    }
}
