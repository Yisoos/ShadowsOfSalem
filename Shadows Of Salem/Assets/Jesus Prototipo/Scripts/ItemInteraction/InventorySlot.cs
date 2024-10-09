using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"item name:{gameObject}");
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryDragging inventorySlot = dropped.GetComponent<InventoryDragging>();
            inventorySlot.parentAfterDrag = transform;
        }
    }
}
