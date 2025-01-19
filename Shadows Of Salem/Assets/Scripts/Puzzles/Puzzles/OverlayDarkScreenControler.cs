using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayDarkScreenControler : MonoBehaviour
{
    private SpriteRenderer overlayDarkScreen;
    private Collider2D overlayCollider;
    private bool isOverlayActive = true;

    void Start()
    {
        overlayDarkScreen = GetComponent<SpriteRenderer>();
        overlayCollider = GetComponent<Collider2D>();
        // que sea visible al inicio el overlay
        ActivateOverlay();
    }
    void Update()
    {
        if (PauseMenu.isPaused) return; ; // Ignorar cualquier input del usuario cuando el juego está en pausa

        if (overlayDarkScreen == null || !isOverlayActive)
            return;

        // Al detectar un clic mientras esta activo el overlay...
        if (Input.GetMouseButtonDown(0))
        {
            // ponemos un raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log($"Hit object: {hit.collider.name}");
            }
        }
    }
    // este metodo se llamará al tener el puzle resuelto
    public void DeactivateOverlay()
    {
        isOverlayActive = false; 
        overlayCollider.enabled = false;
        SetOverlayVisibility(false); 
    }

    private void ActivateOverlay()
    {
        isOverlayActive = true;
        overlayCollider.enabled = true;
        SetOverlayVisibility(true); 
    }
    private void SetOverlayVisibility(bool isVisible)
    {
        overlayDarkScreen.enabled = isVisible; 
    }
    
}
