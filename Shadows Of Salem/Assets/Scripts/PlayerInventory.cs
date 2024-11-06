using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ItemWorld.ItemType, int> inventory = new Dictionary<ItemWorld.ItemType, int>();

    public int maxItems = 10;

    public void AddItem(ItemWorld item)
    {
        int currentItemCount = 0;
        foreach (var kvp in inventory)
        {
            currentItemCount += kvp.Value;
        }

        if (currentItemCount < maxItems)
        {
            if (item.itemType == ItemWorld.ItemType.Sword)
            {
                if (!inventory.ContainsKey(item.itemType))
                {
                    inventory[item.itemType] = 1;
                }
            }
            else
            {
                if (inventory.ContainsKey(item.itemType))
                {
                    inventory[item.itemType] += item.amount;
                }
                else
                {
                    inventory[item.itemType] = item.amount;
                }
            }

            UpdateInventoryUI();
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    // Método para descartar ítems
    public void DiscardItem(ItemWorld.ItemType itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType]--;
            if (inventory[itemType] <= 0)
            {
                inventory.Remove(itemType);
            }
            UpdateInventoryUI();
        }
    }

    public void UseItem(ItemWorld.ItemType itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            if (itemType != ItemWorld.ItemType.Sword)
            {
                inventory[itemType]--;
                if (inventory[itemType] <= 0)
                {
                    inventory.Remove(itemType);
                }
                UpdateInventoryUI();
            }
        }
    }

    private void UpdateInventoryUI()
    {
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateUI();
        }
    }

    public Dictionary<ItemWorld.ItemType, int> GetInventory()
    {
        return inventory;
    }
}



