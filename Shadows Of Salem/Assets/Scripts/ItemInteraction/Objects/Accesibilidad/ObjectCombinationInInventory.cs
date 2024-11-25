using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(Tags))]
public class ObjectCombinationInInventory : MonoBehaviour
{
    Inventory inventory;
    [SerializeField,Tooltip("A�ade aqu� los objetos que est�n por debajo en la gerarqu�a de combinaci�n (Ej. Si el objeto actual es una vela, a�aderias una cerilla)")] public CombinationStatus[] objetosCombinables;
    public Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }

    }
    public bool CheckForCombination(Tags objectA,Tags objectB)
    {
        DarknessDetector detector = FindAnyObjectByType<DarknessDetector>();
        Image objectASprite = objectA.GetComponent<Image>();
        int index = keyValuePairs[objectB.objectName];
        if ( objectA.objectName == objetosCombinables[index].currentItemStatus)
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
