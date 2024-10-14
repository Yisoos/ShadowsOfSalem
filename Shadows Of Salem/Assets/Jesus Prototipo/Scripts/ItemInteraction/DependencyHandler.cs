using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DependencyHandler : MonoBehaviour
{
    [Header("Required Items")]
    public List<string> requiredItems; // List of items needed for this object (e.g., phone, cable, pliers)
    public InventoryOrder inventory; 
    public Sprite phoneWithCable; // The sprite when the cable is connected
    public Sprite phoneWithCableAndPliers; // The final sprite when both items are used
    private SpriteRenderer spriteRenderer; // Reference to change the sprite
                                                                                                                                                                                                                                                                                 
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Check if the required item is part of the dependency list
    public bool HandleItem( Tags objectDropped)
    {
        if (inventory == null)
        {
            Debug.LogError("InventoryOrder reference is missing!");
            return false;
        }
        // Check if the droppedItem's objectName exists in the requiredItems list
        if (!requiredItems.Contains(objectDropped.objectName))
        {
            Debug.Log($"Dropped item '{objectDropped.objectName}' is not on the required items list.");
            return false;
        }
        // Iterate through the required items and check if they all exist in the inventory
        foreach (string requiredItem in requiredItems)
        {
            // Check if the inventory contains the required item
            bool itemFound = inventory.items.Exists(item => item.objectName == requiredItem);

            // If any required item is not found, return false
            if (!itemFound)
            {
                Debug.Log($"Item {requiredItem} not found in inventory.");
                return false;
            }
        }

        // All required items were found
        Debug.Log("All required items are present in the inventory.");
        return true;
    }
}
