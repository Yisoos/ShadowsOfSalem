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

    private void Start()
    {
        // Iniciar la música de fondo con un delay
        StartCoroutine(PlayBackgroundMusicAfterDelay(backgroundDelay));

        // Iniciar el timbre con un delay
        StartCoroutine(PlayTimbreAfterDelay(timbreDelay));
    }

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
        }
        else
        {
            Debug.LogWarning("No se asignó un clip de sonido para el timbre.");
        }
    }
}
