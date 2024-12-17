using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewInventory : MonoBehaviour
{
    // Array de espacios de inventario en la UI
    public GameObject[] inventorySlots;

    // Lista para almacenar los objetos en el inventario
    public List<InventoryItem> items = new List<InventoryItem>();

    private void Start()
    {
        // Inicializar la lista de objetos recorriendo cada espacio de inventario
        for (int i = 0; i < inventorySlots.Length; i++)
        {
                // Obtener todos los objetos de tipo Tags en los hijos del espacio de inventario
                InventoryItem[] slotItems = inventorySlots[i].GetComponentsInChildren<InventoryItem>();

                // Añadir los objetos encontrados a la lista de items
                items.AddRange(slotItems);
        }
    }

    // Método para recolectar un objeto y agregarlo al inventario
    public bool CollectItem(InventoryItem itemPrefab, NewTags origin)
    { 
        // Buscar si el objeto ya existe en el inventario i
        InventoryItem itemInList = items.Find(currentItem => currentItem.tagInfo.objectName == itemPrefab.tagInfo.objectName);
        if (itemInList != null)
        {
            // Si el objeto ya está en el inventario, aumentar su cantidad
            itemInList.tagInfo.quantity += itemPrefab.tagInfo.quantity;

            // Actualizar el texto en la UI con la nueva cantidad
            TMP_Text itemText = itemInList.GetComponentInChildren<TMP_Text>();
            if (itemText != null)
            {
                itemText.text = itemInList.tagInfo.quantity.ToString();
            }
            return true;
        }
        else
        {
            // Buscar un espacio vacío en el inventario
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].transform.childCount == 0)
                {
                    // Instanciar el objeto en el espacio vacío
                    GameObject item = Instantiate(itemPrefab.gameObject, inventorySlots[i].transform);
                    SetPrefabSpecifications(item, origin);
                    // Añadir el objeto al inventario
                    InventoryItem itemTags = item.GetComponent<InventoryItem>();
                    items.Add(itemTags);
                    return true; // Salir después de añadir el objeto
                }
            }
        }
        return false; // Inventario lleno
    }

    // Método para cambiar el estado y sprite de un objeto
    public void ChangeItemStatus(InventoryItem item, string newStatus, Sprite newSprite)
    {
        // Buscar el objeto en el inventario
        InventoryItem inventoryItem = items.Find(currentItem => currentItem.tagInfo.objectName == item.tagInfo.objectName);
        if (inventoryItem != null)
        {
            // Actualizar nombre y sprite del objeto
            inventoryItem.tagInfo.objectName = newStatus;
            inventoryItem.tagInfo.sprite = newSprite;

            // Actualizar la imagen del espacio en la UI
            Image slotItemImage = inventoryItem.transform.GetComponent<Image>();
            slotItemImage.sprite = newSprite;
        }
    }

    // Método para eliminar o reducir la cantidad de un objeto
    public void DeleteItem(InventoryItem itemPrefab)
    {
        int deletedAmount = 1;

        // Verificar si el objeto no es reutilizable
        if (itemPrefab.tagInfo.objectType != TypeObject.Reusable)
        {
            // Buscar el objeto en el inventario
            InventoryItem inventoryItem = items.Find(currentItem => currentItem.tagInfo.objectName == itemPrefab.tagInfo.objectName);
            if (inventoryItem != null)
            {
                if (inventoryItem.tagInfo.quantity - deletedAmount > 0)
                {
                    // Reducir la cantidad del objeto
                    inventoryItem.tagInfo.quantity -= deletedAmount;

                    // Actualizar la cantidad en la UI
                    TMP_Text itemText = inventoryItem.GetComponentInChildren<TMP_Text>();
                    if (itemText != null)
                    {
                        itemText.text = inventoryItem.tagInfo.quantity.ToString();
                    }
                }
                else
                {
                    // Eliminar el objeto si la cantidad llega a cero
                    Destroy(inventoryItem.gameObject);
                    items.Remove(inventoryItem);
                }
            }
        }
    }

    // Configurar las especificaciones del prefab del objeto
    public void SetPrefabSpecifications(GameObject item, NewTags originTags)
    {
        // Actualizar sprite y nombre según el estado actual del objeto
        Image prefabSprite = item.GetComponent<Image>();
        InventoryItem prefabTags = item.GetComponent<InventoryItem>();
        ClassSummoner classSummoner = item.GetComponent<ClassSummoner>();
        
        prefabSprite.sprite = originTags.sprite;
        prefabTags.tagInfo.sprite = originTags.sprite;
        prefabTags.tagInfo.objectName = originTags.objectName;

        if(classSummoner != null)
        {
            classSummoner.summonOrigin = originTags.transform;
        }
    }
}
