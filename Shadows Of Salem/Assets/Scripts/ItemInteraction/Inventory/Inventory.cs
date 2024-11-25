using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Array de espacios de inventario en la UI
    public GameObject[] inventorySlots;

    // Lista para almacenar los objetos en el inventario
    public List<Tags> items = new List<Tags>();

    private void Start()
    {
        // Inicializar la lista de objetos recorriendo cada espacio de inventario
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            GameObject slot = inventorySlots[i];

            if (slot != null)
            {
                // Obtener todos los objetos de tipo Tags en los hijos del espacio de inventario
                Tags[] slotItems = slot.GetComponentsInChildren<Tags>();

                // Añadir los objetos encontrados a la lista de items
                items.AddRange(slotItems);
            }
            else
            {
                // Advertencia si un espacio de inventario está nulo
                Debug.LogWarning($"El espacio de inventario en el índice {i} es nulo. Verifica la configuración en el inspector.");
            }
        }
    }

    // Método para recolectar un objeto y agregarlo al inventario
    public bool CollectItem(Tags itemPrefab, Tags origin)
    {
        // Buscar si el objeto ya existe en el inventario
        Tags itemInList = items.Find(currentItem => currentItem.objectName == itemPrefab.objectName);
        if (itemInList != null)
        {
            // Si el objeto ya está en el inventario, aumentar su cantidad
            itemInList.quantity += itemPrefab.quantity;

            // Actualizar el texto en la UI con la nueva cantidad
            TMP_Text itemText = itemInList.GetComponentInChildren<TMP_Text>();
            if (itemText != null)
            {
                itemText.text = itemInList.quantity.ToString();
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
                    Tags itemTags = item.GetComponent<Tags>();
                    items.Add(itemTags);
                    return true; // Salir después de añadir el objeto
                }
            }
        }
        return false; // Inventario lleno
    }

    // Método para cambiar el estado y sprite de un objeto
    public void ChangeItemStatus(Tags item, string newStatus, Sprite newSprite)
    {
        // Buscar el objeto en el inventario
        Tags inventoryItem = items.Find(currentItem => currentItem.objectName == item.objectName);
        if (inventoryItem != null)
        {
            // Actualizar nombre y sprite del objeto
            inventoryItem.objectName = newStatus;
            inventoryItem.sprite = newSprite;

            // Actualizar la imagen del espacio en la UI
            Image slotItemImage = inventoryItem.transform.GetComponent<Image>();
            slotItemImage.sprite = newSprite;
        }
    }

    // Método para eliminar o reducir la cantidad de un objeto
    public void DeleteItem(Tags itemPrefab)
    {
        int deletedAmount = 1;

        // Verificar si el objeto no es reutilizable
        if (itemPrefab.objectType != ObjectType.Reusable)
        {
            // Buscar el objeto en el inventario
            Tags inventoryItem = items.Find(currentItem => currentItem.objectName == itemPrefab.objectName);
            if (inventoryItem != null)
            {
                if (inventoryItem.quantity - deletedAmount > 0)
                {
                    // Reducir la cantidad del objeto
                    inventoryItem.quantity -= deletedAmount;

                    // Actualizar la cantidad en la UI
                    TMP_Text itemText = inventoryItem.GetComponentInChildren<TMP_Text>();
                    if (itemText != null)
                    {
                        itemText.text = inventoryItem.quantity.ToString();
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
    public void SetPrefabSpecifications(GameObject item, Tags originTags)
    {
       
            // Actualizar sprite y nombre según el estado actual del objeto
        Image prefabSprite = item.GetComponent<Image>();
        Tags prefabTags = item.GetComponent<Tags>();
        prefabSprite.sprite = originTags.sprite;
        prefabTags.sprite = originTags.sprite;
        prefabTags.objectName = originTags.objectName;
    }
}
