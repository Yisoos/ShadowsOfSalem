using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip ClickButton;
    public AudioClip saltoDeEscritura;
    public AudioClip Inicio_Button;
    public AudioClip AjustesButton;
    public AudioClip SalirButton;
    public AudioClip timbre; // Sonido del timbre

    private void Start()
    {
        // Reproducir música de fondo al inicio
        if (background != null)
        {
            musicSource.clip = background;
            musicSource.Play();
        }

        // Iniciar corutina para el timbre después de 3 segundos
        StartCoroutine(PlayTimbreAfterDelay(3f));
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
