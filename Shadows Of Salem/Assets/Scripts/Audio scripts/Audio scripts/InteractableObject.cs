using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Asignamos el sonido a reproducir cuando se abre o cierra el objeto
    public bool isOpened = false;  // Estado para saber si el objeto está abierto o cerrado
    public AudioClip openSound;    // Sonido para abrir
    public AudioClip closeSound;   // Sonido para cerrar

    // Método para manejar la interacción (clic)
    private void OnMouseDown()
    {
        // Llamamos a la función que maneja la interacción
        Interactuar();
    }

    private void Interactuar()
    {
        if (AccesibilityChecker.Instance.IsAccessibleOnMousedown(this.transform))
        {
            // Si el objeto está cerrado, abrirlo y reproducir el sonido de apertura
            if (!isOpened)
            {
                // Aquí pondríamos la lógica para abrir la carta o cajón (animaciones, cambios de estado, etc.)
                AudioManager.Instance.PlaySFX(openSound);  // Reproducir el sonido de abrir
                isOpened = true;
            }
            else
            {
                // Aquí pondríamos la lógica para cerrar la carta o cajón (animaciones, cambios de estado, etc.)
                AudioManager.Instance.PlaySFX(closeSound);  // Reproducir el sonido de cerrar
                isOpened = false;
            }
        }
    }
}
