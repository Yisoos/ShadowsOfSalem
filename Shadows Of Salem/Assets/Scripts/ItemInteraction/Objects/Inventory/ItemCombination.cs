using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tags))]
public class ItemCombination : MonoBehaviour
{
    [SerializeField,Tooltip("A�ade aqu� los objetos que est�n por debajo en la gerarqu�a de combinaci�n (Ej. Si el objeto actual es una vela, a�aderias una cerilla)")] CombinationStatus[] objetosCombinables;
    public Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();
    private void Start()
    {
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }
    }
    public bool CheckForCombination(Tags objectA,Tags objectB)
    {
        if (objetosCombinables[keyValuePairs[objectB.objectName]].currentItemName == objectA.objectName)
        {
            Image itemImage = GetComponent<Image>();
            int index = keyValuePairs[objectB.objectName];
            itemImage.sprite = objetosCombinables[index].newItemStatusSprite;
            objectA.objectName = objetosCombinables[index].newItemName;
            Destroy(objectB.gameObject);
            return true;
        }
        
            return false;
    }
}
