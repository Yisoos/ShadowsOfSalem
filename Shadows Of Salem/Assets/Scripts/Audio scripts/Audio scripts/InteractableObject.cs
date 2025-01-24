using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Asignamos el sonido a reproducir cuando se abre o cierra el objeto
    public bool isOpened = false;  // Estado para saber si el objeto est� abierto o cerrado
    public AudioClip openSound;    // Sonido para abrir
    public AudioClip closeSound;   // Sonido para cerrar

    // M�todo para manejar la interacci�n (clic)
    private void OnMouseDown()
    {
        // Llamamos a la funci�n que maneja la interacci�n
        Interactuar();
    }

    private void Interactuar()
    {
        if (AccesibilityChecker.Instance.IsAccessibleOnMousedown(this.transform))
        {
            // Si el objeto est� cerrado, abrirlo y reproducir el sonido de apertura
            if (!isOpened)
            {
                // Aqu� pondr�amos la l�gica para abrir la carta o caj�n (animaciones, cambios de estado, etc.)
                AudioManager.Instance.PlaySFX(openSound);  // Reproducir el sonido de abrir
                isOpened = true;
            }
            else
            {
                // Aqu� pondr�amos la l�gica para cerrar la carta o caj�n (animaciones, cambios de estado, etc.)
                AudioManager.Instance.PlaySFX(closeSound);  // Reproducir el sonido de cerrar
                isOpened = false;
            }
        }
    }
}
