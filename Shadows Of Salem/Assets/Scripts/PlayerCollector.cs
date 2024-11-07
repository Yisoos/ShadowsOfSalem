using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public PlayerInventory playerInventory;  // Referencia al inventario del jugador

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto con el que colisiona es un ítem
        ItemWorld item = other.GetComponent<ItemWorld>();
        if (item != null)
        {
            // Llama al método para agregar el ítem al inventario
            playerInventory.AddItem(item);

            // Destruye el ítem en la escena para simular que ha sido recogido
            Destroy(other.gameObject);
        }
    }
}
