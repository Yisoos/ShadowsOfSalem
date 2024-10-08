using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
   public GameObject itemPrefab; // Prefab del objeto que se recogerá

    private void OnMouseDown()
    {
        // Obtener la instancia de InventoryOrder
        InventoryOrder inventory = FindObjectOfType<InventoryOrder>();
        if (inventory != null)
        {
            inventory.CollectItem(itemPrefab);
            Debug.Log($"Collected {itemPrefab.name}");
            // Destruir el objeto de la escena
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("InventoryOrder not found!");
        }
    }
}
