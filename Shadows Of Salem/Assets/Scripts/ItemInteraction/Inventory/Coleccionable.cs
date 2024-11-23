using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
   public Tags itemPrefab; // Prefab del objeto que se recogerá
    public void OnMouseDown()
    {
        if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform)) 
        {
            CollectItem();
        }
    }

    public void CollectItem() 
    {
        // Obtener la instancia de Inventory
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.CollectItem(itemPrefab, transform);
            //Debug.Log($"Collected {itemPrefab.name}");
            // Destruir el objeto de la escena
            MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
            if (multipleViewItem != null)
            {
                multipleViewItem.HideObjectsInAllViews();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("InventoryOrder not found!");
        }
    }
}
