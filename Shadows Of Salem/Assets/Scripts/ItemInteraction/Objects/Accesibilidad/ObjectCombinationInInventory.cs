using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tags))]
public class ObjectCombinationInInventory : MonoBehaviour
{
    NewInventory inventory;
    [SerializeField,Tooltip("Añade aquí los objetos que están por debajo en la gerarquía de combinación (Ej. Si el objeto actual es una vela, añaderias una cerilla)")] public CombinationStatus[] objetosCombinables;
    public Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();
    private void Start()
    {
        inventory = FindObjectOfType<NewInventory>();
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }

    }
    public bool CheckForCombination( InventoryItem objectA, InventoryItem objectB)
    {
        DarknessDetector detector = FindAnyObjectByType<DarknessDetector>();
        Image objectASprite = objectA.GetComponent<Image>();
        int index = keyValuePairs[objectB.tagInfo.objectName];
        if ( objectA.tagInfo.objectName == objetosCombinables[index].currentItemStatus)
        {
            inventory.ChangeItemStatus(objectA, objetosCombinables[index].newItemStatus, objetosCombinables[index].newItemStatusSprite);
            inventory.DeleteItem(objectB);
            if (detector != null && detector.gameObject.activeSelf) 
            {
                detector.CheckForLight();
            }
            return true;
        }

        return false;
    }
}
