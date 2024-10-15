using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryOrder : MonoBehaviour
{
    public GameObject[] inventorySlot;  // Array of inventory slots in the UI
    public List<Tags> items = new List<Tags>();  // List to hold items in the inventory

    private void Start()
    {
        // Iterate through each inventory slot using a for loop
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            GameObject slot = inventorySlot[i];

            if (slot != null)
            {
                // Get all Tags components in the children of the slot
                Tags[] slotItems = slot.GetComponentsInChildren<Tags>();

                // Add them to the items list
                items.AddRange(slotItems);
            }
            else
            {
                Debug.LogWarning($"Inventory slot at index {i} is null. Please check the setup in the inspector.");
            }
        }

        Debug.Log($"Inventory initialized with {items.Count} items.");
    }

    // Function to collect an item
    public void CollectItem(Tags itemPrefab)
    {
        //Debug.Log("CollectItem function activated");

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            // Check if the inventory slot is empty (no children)
            if (inventorySlot[i].transform.childCount == 0)
            {
                // Check if the item already exists in the inventory
                Tags itemInList = items.Find(i => i.objectName == itemPrefab.objectName);
                if (itemInList != null)
                {
                    // If the item exists, increase its quantity
                    itemInList.quantity += itemPrefab.quantity;
                }
                else
                {
                    // If it's a new item, add it to the inventory
                    items.Add(itemPrefab);
                    Debug.Log($"{itemPrefab} added to inventory");
                }

                // Instantiate the itemPrefab in the inventory slot
                GameObject item = Instantiate(itemPrefab.gameObject, inventorySlot[i].transform);

                // Update the TMP_Text in the inventory slot to reflect the quantity
                TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                if (itemText != null)
                {
                    itemText.text = itemPrefab.quantity.ToString();
                }

                Debug.Log($"{itemPrefab.objectName} collected");
                break;
            }
        }
    }

    // Function to delete or decrease an item
    public void DeleteItem(Tags itemPrefab)
    {
        if(itemPrefab.category != ObjectCategory.Tool) 
        {
            Debug.Log("Delete item");
        }
        else
        {
            Debug.Log("Item is tool");
        }
    }
}
