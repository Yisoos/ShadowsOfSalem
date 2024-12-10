using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public InventoryItem itemPrefab;

    [SerializeField] NewTags inheritTags;

    public void OnMouseDown()
    {   if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform)) 
        {
            CollectItem();
        }
    }

    public void CollectItem() 
    {
        // Obtener la instancia de Inventory
        NewInventory inventory = FindObjectOfType<NewInventory>();
        if (inventory != null)
        {
            if (inventory.CollectItem(itemPrefab, inheritTags))
            {
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
                Debug.Log("Inventario esta lleno");
            }
        }
        else
        {
            Debug.LogError("InventoryOrder not found!");
        }
    }
}
