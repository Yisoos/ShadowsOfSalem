using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InterchangableItemPlacement : MonoBehaviour
{
    public Tags placeholder;
    public InventoryItem itemPrefab;
    public string[] itemsToPlace;
    [HideInInspector] public Tags thisTag;
    private Inventory inventory;
    private SpriteRenderer thisRenderer;
    private MultipleViewItem multipleViewItem;
    private void Start()
    {
        placeholder.transform = transform;
        thisTag = placeholder;
        inventory = FindAnyObjectByType<Inventory>();
        thisRenderer = GetComponent<SpriteRenderer>();
        multipleViewItem = GetComponent<MultipleViewItem>();
    }
    public void OnMouseDown()
    {
        if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        RemoveItemsFromPlace();
    }
    public void RemoveItemsFromPlace() 
    {
        if(thisTag != placeholder)
        {
            if (inventory.CollectItem(itemPrefab,thisTag,1)) 
            {
                thisTag = placeholder;
                thisRenderer.sprite = thisTag.sprite;
                if (multipleViewItem != null)
                {
                    multipleViewItem.UpdateMultipleViews(Array.IndexOf(itemsToPlace,thisTag.objectName));
                }
            }
        }
    }
    public void PlaceOrSwapItem(InventoryItem itemDropped)
    {
        if (itemsToPlace.Contains(itemDropped.tagInfo.objectName))
        {
            if (thisTag == placeholder)
            {
                thisTag = itemDropped.tagInfo;
                thisRenderer.sprite = itemDropped.tagInfo.sprite;
                inventory.DeleteItem(itemDropped, 1);
            }
            else
            {
                StartCoroutine(SwapItemCoroutine(itemDropped));
            }

            if (multipleViewItem != null)
            {
                multipleViewItem.UpdateMultipleViews(System.Array.IndexOf(itemsToPlace, thisTag.objectName));
            }
        }
    }

    private IEnumerator SwapItemCoroutine(InventoryItem itemDropped)
    {
        inventory.DeleteItem(itemDropped, 1);
        // Wait until the next frame
        yield return null;

        // Attempt to collect the current item
        if (inventory.CollectItem(itemPrefab, thisTag, 1))
        {
            // Update thisTag and sprite
            thisTag = itemDropped.tagInfo;
            thisRenderer.sprite = thisTag.sprite;
        }
        else
        {
            inventory.CollectItem(itemPrefab, itemDropped.tagInfo, 1);
        }
    }
}

