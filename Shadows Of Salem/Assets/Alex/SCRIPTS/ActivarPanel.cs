using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPanel : MonoBehaviour
{
    public GameObject panel; // Asignar panel
    private void OnMouseDown()
    {
        TogglePanel();
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Activar panel");
        }
    }
}
