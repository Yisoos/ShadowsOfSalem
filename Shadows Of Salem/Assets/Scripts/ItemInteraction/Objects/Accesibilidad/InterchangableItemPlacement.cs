using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterchangableItemPlacement : MonoBehaviour
{
    public InventoryItem itemPrefab;
    public string[] itemsToPlace;
    public NewTags thisTag;
    
    public void OnMouseDown()
    {
        if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        RemoveItemsFromPlace();
    }
    public void RemoveItemsFromPlace() 
    {
        MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
        NewInventory inventory = FindAnyObjectByType<NewInventory>();
        SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
        if(thisTag.objectName != itemsToPlace[0])
        {
            if (inventory.CollectItem(itemPrefab,thisTag)) 
            {
                thisTag.objectName = itemsToPlace[0];
                thisTag.sprite = itemPrefab.GetComponent<Tags>().sprite;
                thisRenderer.sprite = itemPrefab.GetComponent<Tags>().sprite;
                if (multipleViewItem != null && Array.IndexOf(itemsToPlace, thisTag.objectName) >= 0)
                {
                    multipleViewItem.UpdateMultipleViews(Array.IndexOf(itemsToPlace,thisTag.objectName));
                }
            }
        }
    }
    public void PlaceOrSwapItem(InventoryItem itemDropped)
    {
        NewInventory inventory = FindAnyObjectByType<NewInventory>();
        if (Array.Exists(itemsToPlace, itemInList => itemInList == itemDropped.tagInfo.objectName))
        {
            MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
            Tags thisTag = GetComponent<Tags>();
            SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
            Debug.Log($"thisTag.objectName: {thisTag.objectName}");
            Debug.Log($"itemsToPlace[0]: {itemsToPlace[0]}");
            Debug.Log($"Comparison result: {thisTag.objectName == itemsToPlace[0]}");
            if (thisTag.objectName == itemsToPlace[0])
            {
                thisTag.objectName = itemDropped.tagInfo.objectName;
                thisTag.sprite = itemDropped.tagInfo.sprite;
                thisRenderer.sprite = itemDropped.tagInfo.sprite;
                inventory.DeleteItem(itemDropped);
            }
            else
            {
                string currentName = thisTag.objectName;
                Sprite currentSprite = thisRenderer.sprite;
                InventoryItem droppedSprite = itemDropped.GetComponent<InventoryItem>();
                thisTag.objectName = itemDropped.tagInfo.objectName;
                thisTag.sprite = itemDropped.tagInfo.sprite;
                thisRenderer.sprite = itemDropped.tagInfo.sprite;
                itemDropped.tagInfo.objectName = currentName;
                itemDropped.tagInfo.sprite = currentSprite;
                droppedSprite.tagInfo.sprite = currentSprite;
            }
            if (multipleViewItem != null && Array.IndexOf(itemsToPlace, thisTag.objectName) >= 0)
            {
                multipleViewItem.UpdateMultipleViews(Array.IndexOf(itemsToPlace, thisTag.objectName));
            }
        }  
    }
}
