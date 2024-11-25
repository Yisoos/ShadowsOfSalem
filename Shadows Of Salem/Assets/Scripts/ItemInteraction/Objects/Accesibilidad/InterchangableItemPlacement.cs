using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tags))]
public class InterchangableItemPlacement : MonoBehaviour
{
    public GameObject itemPrefab;
    public string[] itemsToPlace;
    
    public void OnMouseDown()
    {
        if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        RemoveItemsFromPlace();
    }
    public void RemoveItemsFromPlace() 
    {
        MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
        Inventory inventory = FindAnyObjectByType<Inventory>();
        Tags thisTag = GetComponent<Tags>();
        SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
        if(thisTag.objectName != itemsToPlace[0])
        {
            if (inventory.CollectItem(itemPrefab.GetComponent<Tags>(),thisTag)) 
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
    public void PlaceOrSwapItem(Tags itemDropped)
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();
        if (Array.Exists(itemsToPlace, itemInList => itemInList == itemDropped.objectName))
        {
            MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
            Tags thisTag = GetComponent<Tags>();
            SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
            Debug.Log($"thisTag.objectName: {thisTag.objectName}");
            Debug.Log($"itemsToPlace[0]: {itemsToPlace[0]}");
            Debug.Log($"Comparison result: {thisTag.objectName == itemsToPlace[0]}");
            if (thisTag.objectName == itemsToPlace[0])
            {
                thisTag.objectName = itemDropped.objectName;
                thisTag.sprite = itemDropped.sprite;
                thisRenderer.sprite = itemDropped.sprite;
                inventory.DeleteItem(itemDropped);
            }
            else
            {
                string currentName = thisTag.objectName;
                Sprite currentSprite = thisRenderer.sprite;
                Image droppedSprite = itemDropped.GetComponent<Image>();
                thisTag.objectName = itemDropped.objectName;
                thisTag.sprite = itemDropped.sprite;
                thisRenderer.sprite = itemDropped.sprite;
                itemDropped.objectName = currentName;
                itemDropped.sprite = currentSprite;
                droppedSprite.sprite = currentSprite;
            }
            if (multipleViewItem != null && Array.IndexOf(itemsToPlace, thisTag.objectName) >= 0)
            {
                multipleViewItem.UpdateMultipleViews(Array.IndexOf(itemsToPlace, thisTag.objectName));
            }
        }  
    }
}
