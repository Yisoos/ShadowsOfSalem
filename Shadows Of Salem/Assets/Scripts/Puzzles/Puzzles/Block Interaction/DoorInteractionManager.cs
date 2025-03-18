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


    private void Start()
    {
        // desactivar al principio el collider de la puerta
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        StartCoroutine(PlayDoorbellWithDelay());
    }


    private IEnumerator PlayDoorbellWithDelay()
    {
        yield return new WaitForSeconds(2f);

        SoundManager.Instance.PlaySound("Timbre", SoundType.SFX);

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
        }
    }
}
