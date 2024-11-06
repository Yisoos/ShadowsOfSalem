using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public enum ItemType
    {
        Potion,
        Coin,
        Sword,
        Box
    }

    public ItemType itemType;
    public int amount; // For stackable items like potions and coins

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(this);
                Destroy(gameObject);
            }
        }
    }
}
