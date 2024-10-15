using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliicColliders : MonoBehaviour
{
    public GameObject cajon1; // Asignar los paneles
    public GameObject cajon2; 

    private void OnMouseDown()
    {
        Debug.Log("Clic detectado en: " + gameObject.name);

        // Verificar si el clic fue en collider1
        if (gameObject.name == "Collider1")
        {
            TogglePanel(cajon1);
        }
        // Verificar si el clic fue en collider2
        else if (gameObject.name == "Collider2")
        {
            TogglePanel(cajon2);
        }
    }

    void TogglePanel(GameObject cajon)
    {
        if (cajon != null)
        {
            cajon.SetActive(!cajon.activeSelf);
            Debug.Log("Activar Panel" + cajon.name);
        }
    }
}
