using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem itemDroppedInfo = dropped.GetComponent<InventoryItem>();
        if (transform.childCount == 0)
        {
            itemDroppedInfo.parentAfterDrag = transform;
        }
        else
        {
            InventoryItem existingItemInSlot = GetComponentInChildren<InventoryItem>();
            
            if (existingItemInSlot != null && AccesibilityChecker.Instance.isUIObjectInteractable(itemDroppedInfo, existingItemInSlot))
            {
                if (transform.childCount == 0)
                itemDroppedInfo.parentAfterDrag = transform;
            }
        }
    }
}
