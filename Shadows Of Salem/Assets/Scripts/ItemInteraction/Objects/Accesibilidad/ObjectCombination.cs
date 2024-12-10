using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectCombination : MonoBehaviour
{
    [SerializeField] public NewTags thisTag;
    [SerializeField, Tooltip("Añade aquí los objetos que están por debajo en la gerarquía de combinación (Ej. Si el objeto actual es una vela, añaderias una cerilla)")] CombinationStatus[] objetosCombinables;
    public NewInventory inventory;
    Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();

    private void Start()
    {
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }
    }
    public bool CheckForCombination(InventoryItem ObjectDropped)
    {
        if (objetosCombinables[keyValuePairs[ObjectDropped.tagInfo.objectName]].currentItemStatus == thisTag.objectName)
        {
            SpriteRenderer itemImage = GetComponent<SpriteRenderer>();
            MultipleViewItem multipleViewChange = GetComponent<MultipleViewItem>();
            int index = keyValuePairs[ObjectDropped.tagInfo.objectName];
            itemImage.sprite = objetosCombinables[index].newItemStatusSprite;
            thisTag.objectName = objetosCombinables[index].newItemStatus;
            thisTag.sprite = objetosCombinables[index].newItemStatusSprite;
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
            inventory.DeleteItem(ObjectDropped); 
            return true;
        }

        return false;
    }
}
