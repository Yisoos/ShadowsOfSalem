using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public PlayerInventory playerInventory;  // Referencia al inventario del jugador

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto con el que colisiona es un �tem
        ItemWorld item = other.GetComponent<ItemWorld>();
        if (item != null)
        {
            // Llama al m�todo para agregar el �tem al inventario
            playerInventory.AddItem(item);

            // Destruye el �tem en la escena para simular que ha sido recogido
            Destroy(other.gameObject);
        }
    }
}
