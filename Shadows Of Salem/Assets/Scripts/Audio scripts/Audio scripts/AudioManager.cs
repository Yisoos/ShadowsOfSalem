using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Datos de Audio")]
    public AudioManagerData audioData; // El ScriptableObject donde se almacenan los clips

    [Header("Fuentes de Audio")]
    [SerializeField] private AudioSource musicSource;  // Fuente de música
    [SerializeField] private AudioSource SFXSource;    // Fuente de efectos de sonido

    [Header("Volumen")]
    [Range(0f, 1f)] public float volumeSFX = 1f; // Volumen de efectos de sonido
    [Range(0f, 1f)] public float volumeMusic = 1f; // Volumen de música de fondo


    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina el AudioManager duplicado
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }

        // Establecer volúmenes al iniciar
        SetVolumeSFX(volumeSFX);
        SetVolumeMusic(volumeMusic);
    }

    private void Start()
    {
        // Asignar y reproducir música de fondo si se ha configurado audioData
        if (audioData != null && musicSource != null)
        {
            musicSource.clip = audioData.backgroundINTRO;
            musicSource.Play();
        }
    }

    // Método para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip); // Reproducir el efecto de sonido
        }
    }

    // Método para pausar el audio (cuando se cambia de escena, por ejemplo)
    public void PauseAudio()
    {
        AudioListener.pause = true;
        musicSource.Pause(); // Pausa la música de fondo si está sonando
    }

    // Método para reanudar el audio
    public void ResumeAudio()
    {
        AudioListener.pause = false;
        musicSource.Play(); // Reanuda la música de fondo si estaba pausada
    }

    public void StopPlayingAudio() 
    {
        Debug.Log("stopping");

        SFXSource.Stop();
        musicSource.Stop();
    }

    // Método para ajustar el volumen de los efectos de sonido
    public void SetVolumeSFX(float volume)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    // Método para ajustar el volumen de la música de fondo
    public void SetVolumeMusic(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    // Cambia la música de fondo entre escenas
    public void ChangeBackgroundMusic(AudioClip newBackgroundClip)
    {
        if (musicSource != null && newBackgroundClip != null)
        {
            musicSource.clip = newBackgroundClip;
            musicSource.Play();
        }
    }
}
