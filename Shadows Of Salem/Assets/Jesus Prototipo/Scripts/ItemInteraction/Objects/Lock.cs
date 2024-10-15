using UnityEngine;

// Clase que representa un candado
public class Lock : MonoBehaviour
{
    public int lockID; // El ID de este candado
    public bool isLocked; // Indica si el candado está cerrado
    public bool isPhysicalLock; // Indica si es un candado físico
    public bool isCombinationLock; // Indica si es un candado de combinación
    public int combination; // La clave que desbloquea este candado
    public Inventory inventoryOrder; // Referencia al inventario asociado

    // Método que intenta desbloquear el candado con una llave
    public void TryUnlock(Key key)
    {
        // Verifica si la llave no es nula, si su ID coincide con el del candado y si está bloqueado
        if (key != null && key.keyID == lockID && isLocked)
        {
            Debug.Log("¡Candado abierto!"); // Mensaje de éxito
            isLocked = false; // Cambia el estado del candado a desbloqueado

            // Si es un candado físico, lo desactiva
            if (isPhysicalLock)
            {
                gameObject.SetActive(false);
            }

            // Obtiene el componente Tags de la llave y elimina la llave del inventario
            Tags keyTag = key.gameObject.GetComponent<Tags>();
            inventoryOrder.DeleteItem(keyTag);
        }
        else
        {
            Debug.Log("La llave no coincide con la requerida."); // Mensaje de error
        }
    }
}
