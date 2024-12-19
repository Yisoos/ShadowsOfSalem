using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectCombination : MonoBehaviour
{
    [Tooltip("Añade aquí los objetos que están por debajo en la gerarquía de combinación (Ej. Si el objeto actual es una vela, añaderias una cerilla)")] public CombinationStatus[] objetosCombinables;
    private NewInventory inventory;
    private ItemCollection thisTag;
    Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();

    private void Start()
    {
        inventory = FindAnyObjectByType<NewInventory>();
        thisTag = GetComponent<ItemCollection>();
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }
    }
    public bool CheckForCombination(InventoryItem ObjectDropped)
    {
        if (thisTag != null && objetosCombinables[keyValuePairs[ObjectDropped.tagInfo.objectName]].currentItemStatus == thisTag.inheritTags.objectName)
        {
            SpriteRenderer itemImage = GetComponent<SpriteRenderer>();
            MultipleViewItem multipleViewChange = GetComponent<MultipleViewItem>();
            int index = keyValuePairs[ObjectDropped.tagInfo.objectName];
            itemImage.sprite = objetosCombinables[index].newItemStatusSprite;
            thisTag.inheritTags.objectName = objetosCombinables[index].newItemStatus;
            thisTag.inheritTags.sprite = objetosCombinables[index].newItemStatusSprite;
            if (multipleViewChange != null) 
            {
                for (int i = 0; i < multipleViewChange.objectStatusSprite.Length; i++)
                {
                    if (itemImage.sprite == multipleViewChange.objectStatusSprite[i])
                    {
                        multipleViewChange.UpdateMultipleViews(i);
                    }
                }
            }
            inventory.DeleteItem(ObjectDropped,1); 
            return true;
        }

        return false;
    }
}
