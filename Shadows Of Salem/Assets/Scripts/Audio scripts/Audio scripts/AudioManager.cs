using System.Collections;
using UnityEngine;

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

    [Header("Tiempos de Retraso")]
    public float timbreDelay = 3f;  // Retraso antes de reproducir el timbre

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
            musicSource.clip = audioData.background;
            musicSource.Play();
        }

        // Iniciar la corutina para reproducir el timbre despu�s de un retraso
        StartCoroutine(PlayTimbreAfterDelay(timbreDelay));
    }

    // M�todo para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip); // Reproducir el efecto de sonido
        }
    }

    // M�todo para reproducir el timbre con retraso
    private IEnumerator PlayTimbreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Esperar el tiempo especificado
        if (audioData != null && audioData.timbre != null)
        {
            PlaySFX(audioData.timbre);  // Reproducir el timbre
        }
        else
        {
            Debug.LogWarning("El timbre no est� asignado en AudioManagerData.");
        }
    }

    // M�todos para ajustar el volumen de los efectos de sonido
    public void SetVolumeSFX(float volume)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }

    // M�todos para ajustar el volumen de la m�sica de fondo
    public void SetVolumeMusic(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }
}
