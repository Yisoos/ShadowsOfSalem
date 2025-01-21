using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayDarkScreenControler : MonoBehaviour
{
    private SpriteRenderer overlayDarkScreen;
    private Collider2D overlayCollider;

    private FeedbackTextController feedbackText;
    [Tooltip("Escribir el mensaje que quieres que aparezca al darse click al overlay")]
    public string feedbackMessage;

    [Tooltip("Desactivar los colliders con que no quieres interactuar cuando esté activo el overlay.")]
    public Collider2D[] colliders;
    private bool isOverlayActive = true;

    void Start()
    {
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        SetCollidersActive(false);

        overlayDarkScreen = GetComponent<SpriteRenderer>();
        overlayCollider = GetComponent<Collider2D>();

        // que sea visible al inicio el overlay
        ActivateOverlay();

    }
    void Update()
    {
        if (PauseMenu.isPaused) return; ; // Ignorar cualquier input del usuario cuando el juego está en pausa


        if (!isOverlayActive)
        {
            SetCollidersActive(true);
            return;
        }

        // Al detectar un clic mientras esta activo el overlay...
        if (Input.GetMouseButtonDown(0) && isOverlayActive)
        {
            // ponemos un raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log($"Hit object: {hit.collider.name}");
                feedbackText.PopUpText(feedbackMessage);
            }
        }
    }
    // este metodo se llamará al tener el puzle resuelto
    public void DeactivateOverlay()
    {
        isOverlayActive = false; 
        overlayCollider.enabled = false;
        SetOverlayVisibility(false);
        SetCollidersActive(true);
    }

    private void ActivateOverlay()
    {
        isOverlayActive = true;
        overlayCollider.enabled = true;
        SetOverlayVisibility(true);
        SetCollidersActive(false);
    }
    private void SetOverlayVisibility(bool isVisible)
    {
        overlayDarkScreen.enabled = isVisible; 
    }
    private void SetCollidersActive(bool isActive)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isActive;
        }
    }
}
