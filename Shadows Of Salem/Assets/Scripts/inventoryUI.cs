using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  


public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory; // Referencia al inventario del jugador
    public TMP_Text potionText;                 // Referencia al texto de las pociones
    public TMP_Text coinText;                   // Referencia al texto de las monedas
    public TMP_Text swordText;                  // Referencia al texto de las espadas
    public TMP_Dropdown itemDropdown;           // Dropdown para seleccionar el �tem a descartar

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        UpdateUI();
        PopulateItemDropdown();
    }

    // M�todo para actualizar la UI del inventario
    public void UpdateUI()
    {
        var inventory = playerInventory.GetInventory();

        potionText.text = "Potion: " + (inventory.ContainsKey(ItemWorld.ItemType.Potion) ? inventory[ItemWorld.ItemType.Potion].ToString() : "0");
        coinText.text = "Coin: " + (inventory.ContainsKey(ItemWorld.ItemType.Coin) ? inventory[ItemWorld.ItemType.Coin].ToString() : "0");
        swordText.text = "Sword: " + (inventory.ContainsKey(ItemWorld.ItemType.Sword) ? "1" : "0");

        PopulateItemDropdown();
    }

    // M�todo para usar �tems
    public void UsePotion()
    {
        playerInventory.UseItem(ItemWorld.ItemType.Potion);
        UpdateUI();
    }

    public void UseCoin()
    {
        playerInventory.UseItem(ItemWorld.ItemType.Coin);
        UpdateUI();
    }

    public void UseSword()
    {
        // Implementar l�gica para usar la espada si es necesario
    }

    // M�todo para descartar �tems
    public void DiscardItem()
    {
        ItemWorld.ItemType selectedItem = (ItemWorld.ItemType)System.Enum.Parse(typeof(ItemWorld.ItemType), itemDropdown.options[itemDropdown.value].text);
        playerInventory.DiscardItem(selectedItem);
        UpdateUI();
    }

    // M�todo para llenar el Dropdown con los tipos de �tems
    void PopulateItemDropdown()
    {
        itemDropdown.options.Clear();
        foreach (var itemType in System.Enum.GetValues(typeof(ItemWorld.ItemType)))
        {
            itemDropdown.options.Add(new TMP_Dropdown.OptionData(itemType.ToString()));
        }
        itemDropdown.value = 0;
        itemDropdown.RefreshShownValue();
    }
}
