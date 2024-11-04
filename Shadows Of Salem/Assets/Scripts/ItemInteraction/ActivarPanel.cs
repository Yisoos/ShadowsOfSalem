using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPanel : MonoBehaviour
{
    public GameObject panel; // Asignar panel
    public GameObject arrows; // las flechas
    public GameObject backButton;
    
    public void TogglePanel()
    {
        if (panel != null && arrows != null && backButton != null)  
        {
            bool isPanelActive = !panel.activeSelf;
            panel.SetActive(isPanelActive);
            Debug.Log("Panel activo: " + isPanelActive);

            if (isPanelActive)
            {
                arrows.SetActive(false);  // Desactivar las flechas
                backButton.SetActive(true);
            }
        }

        else
        {
            Debug.LogError("Alguno de los elementos no está asignado.");
        }
            
    }

    public void DeactivatePanel()
    {
        panel.SetActive(false);
        backButton.SetActive(false);
        arrows.SetActive(true);
    }
}
