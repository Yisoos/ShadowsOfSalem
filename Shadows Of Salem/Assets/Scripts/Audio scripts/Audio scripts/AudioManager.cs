using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background; // Música de fondo
    public AudioClip ClickButton;
    public AudioClip saltoDeEscritura;
    public AudioClip Inicio_Button;
    public AudioClip AjustesButton;
    public AudioClip SalirButton;
    public AudioClip timbre; // Sonido del timbre

    [Header("---------- Delay Settings ----------")]
    public float backgroundDelay = 2f; // Tiempo de espera antes de la música
    public float timbreDelay = 3f; // Tiempo de espera antes del timbre

    [Header("---------- Volumen ----------")]
    [Range(0f, 1f)] public float volumeSFX = 1f; // Volumen de los efectos de sonido (0 = silencio, 1 = volumen máximo)
    [Range(0f, 1f)] public float volumeMusic = 1f; // Volumen de la música de fondo (0 = silencio, 1 = volumen máximo)

    [Header("---------- Door/Mail Sprite Changes ----------")]
    public SpriteRenderer doorSpriteRenderer; // hacer referencia al spriteRenderer de la puerta 
    [Tooltip("Asignar el sprite de la puerta con la carta aquí. Cuando el timbre suena, el sprite cambia.")]
    public Sprite doorWithMail; // asignar el sprite de la puerta con la carta aquí 

    private void Start()
    {
        // Iniciar la música de fondo con un delay
        StartCoroutine(PlayBackgroundMusicAfterDelay(backgroundDelay));

        // Iniciar el timbre con un delay
        StartCoroutine(PlayTimbreAfterDelay(timbreDelay));

        // Establecer volúmenes iniciales
        SetVolumeSFX(volumeSFX);
        SetVolumeMusic(volumeMusic);
    }

    // Método para ajustar el volumen de los efectos de sonido
    public void SetVolumeSFX(float volume)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = Mathf.Clamp(volume, 0f, 1f); // Asegura que el volumen esté entre 0 y 1
        }
    }

    // Método para ajustar el volumen de la música de fondo
    public void SetVolumeMusic(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp(volume, 0f, 0.3f); // Asegura que el volumen esté entre 0 y 1
        }
    }

    // Método para reproducir un sonido específico
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip); // Reproduce el efecto de sonido
        }
        else
        {
            Debug.LogWarning("Clip de sonido no asignado.");
        }
    }

    private IEnumerator PlayBackgroundMusicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo indicado
        if (background != null)
        {
            musicSource.clip = background;
            musicSource.Play(); // Reproducir música de fondo
        }
        else
        {
            Debug.LogWarning("No se asignó un clip de música de fondo.");
        }
    }

    private IEnumerator PlayTimbreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo indicado
        if (timbre != null)
        {
            PlaySFX(timbre); // Reproducir el timbre

            if (doorSpriteRenderer != null && doorWithMail != null)
            {
                doorSpriteRenderer.sprite = doorWithMail;
            }
            else
            {
                Debug.LogWarning("No está asignado el doorSpriteRenderer o el doorWithMail.");
            }

            // Activar el collider de la puerta
            ColliderDisabler doorColliderActivator = FindObjectOfType<ColliderDisabler>();
            if (doorColliderActivator != null)
            {
                doorColliderActivator.EnableInteraction();
            }
        }

        else
        {
            Debug.LogWarning("No se asignó un clip de sonido para el timbre.");
        }
    }
}
