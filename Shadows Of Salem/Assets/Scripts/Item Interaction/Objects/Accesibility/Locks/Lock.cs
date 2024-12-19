using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

// Clase que representa un candado
public class Lock : MonoBehaviour
{
    
    public bool isLocked; // Indica si el candado est� cerrado
    public int lockID; // El ID de este candado
    public bool isPhysicalLock; // Indica si es un candado f�sico
    public string[] displayText;
    
    private Inventory inventory; // Referencia al inventario asociado
    private FeedbackTextController feedbackText;

    private void Start()
    {
         inventory = FindAnyObjectByType<Inventory>(); // Referencia al inventario asociado
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
    }

    // M�todo que intenta desbloquear el candado con una llave
    public void TryUnlock(Key key) //para candados con llave
    {
        // Verifica si la llave no es nula, si su ID coincide con el del candado y si est� bloqueado
        if (key != null && key.keyID == lockID)
        {
            if (isLocked) 
            {
            //Debug.Log("�Candado abierto!"); // Mensaje de �xito
            isLocked = false; // Cambia el estado del candado a desbloqueado
            // Si es un candado f�sico, lo desactiva
            if (isPhysicalLock)
            {
                gameObject.SetActive(false);
            }

            // Obtiene el componente Tags de la llave y elimina la llave del inventario
            InventoryItem keyTag = key.GetComponent<InventoryItem>();
            if (feedbackText != null) 
            { 
                feedbackText.PopUpText(displayText[1]);
            }

            inventory.DeleteItem(keyTag,1);
            }
        }
        else
        {
            if (feedbackText != null)
            {
                feedbackText.PopUpText(displayText[0]);
            }
        }
    }
    [ContextMenu("Conectar componentes generales")]
    private void ConectarComponentesGenerales()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        inventory = FindFirstObjectByType<Inventory>();
    }
}
