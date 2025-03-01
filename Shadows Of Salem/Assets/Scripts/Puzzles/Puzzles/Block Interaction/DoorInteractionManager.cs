using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionManager : MonoBehaviour
{
    [Header("Door Sprite Changes")]
    [Tooltip("Arrastra aquí la puerta en la escena")]
    public SpriteRenderer doorSpriteRenderer;
    [Tooltip("Arrastra aquí la puerta en la escena")]
    public Collider2D doorCollider; // collider de la puerta
    [Tooltip("Arrastra el sprite de la puerta con la carta aquí")]
    public Sprite doorWithMail;
    [Header("---------- Delay Settings ----------")]
    public float timbreDelay = 3f; // Tiempo de espera antes del timbre

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        // desactivar al principio el collider de la puerta
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // dontdestroyonload
        if (ObjectInteractionSingleton.Instance != null)
        {
            doorCollider.enabled = ObjectInteractionSingleton.Instance.isColliderEnabled;
        }

        TriggerTimbre();
    }

    public void TriggerTimbre()
    {
        StartCoroutine(PlayTimbreAfterDelay(timbreDelay));
        StartCoroutine(HandleDoorInteraction());
    }

    // la corutina responsable de cambiar el sprite y activar el collider de nuevo
    private IEnumerator HandleDoorInteraction()
    {
        // esperar que termine de sonar el timbr
        yield return new WaitForSeconds(audioManager.audioData.timbre.length);

        // cambiar el sprite
        if (doorSpriteRenderer != null && doorWithMail != null)
        {
            doorSpriteRenderer.sprite = doorWithMail;
            EnableInteraction();
        }
        else
        {
            Debug.LogWarning("Door with mail sprite or sprite renderer was not assigned.");
        }
    }

    // activar el collider de nuevo
    public void EnableInteraction()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
            Debug.Log("Door collider activated.");
            ObjectInteractionSingleton.Instance.isColliderEnabled = true; 
        }
    }

    public IEnumerator PlayTimbreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo indicado
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.audioData.timbre); // Reproducir el timbre
        }

        else
        {
            Debug.LogWarning("No se asignó un clip de sonido para el timbre.");
        }
    }
}
