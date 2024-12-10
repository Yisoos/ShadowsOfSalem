using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggingMechanic draggableItem = dropped.GetComponent<DraggingMechanic>();
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            Tags existingItemInSlot = GetComponentInChildren<Tags>();
            Tags droppedTags = dropped.GetComponent<Tags>();
            if (existingItemInSlot != null && AccesibilityChecker.Instance.isUIObjectInteractable(droppedTags, existingItemInSlot))
            {
                if (transform.childCount == 0)
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}
