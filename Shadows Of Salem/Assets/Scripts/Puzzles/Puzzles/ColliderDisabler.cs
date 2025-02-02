using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisabler : MonoBehaviour
{
    // este script se utiliza para bloquear la interacción del usuario con un objeto en concreto (desactivando su collider) hasta que lo desbloquea (ej: resolviendo puzles)
    private bool canInteract = false; 
    private Collider2D objectCollider; 

    private void Start()
    {
        objectCollider = GetComponent<Collider2D>(); 
        if (objectCollider != null)
        {
            objectCollider.enabled = false; 
        }
    }

    // Llamar esta funcion en el script que contiene la mecanica de tu puzle o lo que sea para que lo vuelve a activar el collider
    public void EnableInteraction()
    {
        canInteract = true;
        if (objectCollider != null)
        {
            objectCollider.enabled = true; 
        }
    }
}
