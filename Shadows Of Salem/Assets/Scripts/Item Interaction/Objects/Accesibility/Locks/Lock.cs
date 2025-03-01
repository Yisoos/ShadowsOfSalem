using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

// Clase que representa un candado en el juego
public class Lock : MonoBehaviour
{
    [Space(5)] public Transform lockedObject; // Objeto que está bloqueado por este candado
    [Space(5)] public int lockID; // Identificador único del candado
    [Space(5)] public Sprite unlockedSprite; // Sprite que se mostrará cuando el candado esté desbloqueado

    [Space(5), Tooltip("Mensajes de retroalimentación: \n" +
                       "0: Necesita una llave/Está cerrado\n" +
                       "1: El objeto usado no es una llave\n" +
                       "2: Llave equivocada\n" +
                       "3: Puerta abierta")]
    public string[] displayText = new string[4];

    [HideInInspector] public bool isLocked = true; // Indica si el candado está cerrado
    private Inventory inventory; // Referencia al inventario del jugador
    private FeedbackTextController feedbackText; // Controlador de mensajes en pantalla
    private LockedObject lockedObjectControl; // Controlador del objeto bloqueado

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Busca en la escena una referencia al inventario y al controlador de mensajes
        inventory = FindAnyObjectByType<Inventory>();
        feedbackText = FindAnyObjectByType<FeedbackTextController>();

        // Si no se ha asignado un sprite de desbloqueo, usa el sprite actual
        if (unlockedSprite == null && spriteRenderer != null)
        {
            unlockedSprite = spriteRenderer.sprite;
        }

        // Si el objeto bloqueado no ha sido asignado, se asigna a este mismo objeto
        if (lockedObject == null)
        {
            lockedObject = this.transform;
        }

        // Obtiene el componente LockedObject, si no lo tiene, lo agrega
        lockedObjectControl = lockedObject.GetComponent<LockedObject>();
        if (lockedObject != this.transform && lockedObjectControl == null)
        {
            lockedObjectControl = lockedObject.AddComponent<LockedObject>();
        }

        // Establece la referencia del candado en el objeto bloqueado
        if (lockedObjectControl != null)
        {
            lockedObjectControl.parentLock = this;
        }
    }

    private void OnMouseDown()
    {
        // Si el juego está pausado, no permite interacción con el candado
        if (PauseMenu.isPaused) return;

        // Verifica la accesibilidad del objeto
        AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.transform);
    }

    // Método que verifica si el candado está bloqueado
    public bool CheckIfLocked()
    {
        FeedbackTextTrigger feedbackTextTrigger = GetComponent<FeedbackTextTrigger>();

        if (isLocked)
        {
            // Si el candado está bloqueado, muestra el mensaje correspondiente
            if (feedbackTextTrigger != null)
            {
                feedbackTextTrigger.DisplayAllText();
            }
            else
            {
                feedbackText.PopUpText(displayText[0]); // "Necesito llave/Está cerrado"
            }
            return false;
        }

        return true; // Retorna true si el candado está desbloqueado
    }

    // Método que intenta desbloquear el candado usando una llave
    public void TryUnlock(InventoryItem itemDropped)
    {
        Key key = itemDropped.GetComponentInParent<Key>();

        // Verifica si el objeto usado es una llave
        if (key != null)
        {
            // Si la llave coincide con el ID del candado
            if (key.keyID == lockID)
            {
                if (isLocked)
                {
                    Unlock();
                    inventory.DeleteItem(itemDropped, 1);
                }
            }
            else
            {
                // Si la llave es incorrecta, muestra un mensaje de error
                feedbackText.PopUpText(displayText[2]); // "Llave equivocada"
            }
        }
        else
        {
            // Si el objeto usado no es una llave, muestra un mensaje de error
            feedbackText.PopUpText(displayText[1]); // "El objeto no es una llave"
        }
    }
    public void Unlock() 
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        isLocked = false; // Desbloquea el candado

        // Cambia el sprite del candado al de desbloqueado
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }

        // Muestra el mensaje de puerta abierta
        if (feedbackText != null)
        {
            feedbackText.PopUpText(displayText[3]);
        }

        // Elimina la llave del inventario
        

        // Si el candado controla otro objeto, lo desbloquea
        if (lockedObject != this.transform)
        {
            lockedObjectControl.OpenUp();
        }

        // Verifica si hay una interacción de acercamiento al objeto
        CloseUpItemInteraction closeUpItemInteraction = transform.parent.GetComponent<CloseUpItemInteraction>();

        if (closeUpItemInteraction != null)
        {
            closeUpItemInteraction.ObjectDetector(GetComponent<Collider2D>());
        }
        else
        {
            closeUpItemInteraction = transform.parent.parent.GetComponent<CloseUpItemInteraction>();
        }

        if (closeUpItemInteraction != null)
        {
            closeUpItemInteraction.ObjectDetector(GetComponent<Collider2D>());
        }
    }
}
