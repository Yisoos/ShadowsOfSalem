using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the inventory item to follow the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Set Z to 0 for 2D
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check for interactions with non-UI objects
        CheckForInteraction(eventData);

        // Snap back to the original parent after dragging
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    private void CheckForInteraction(PointerEventData eventData)
    {
        // Cast a ray from the mouse position in 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Cast ray with zero direction to get the object under the mouse position

        // Check if we hit something
        if (hit.collider != null)
        {
            // Check if the hit object has the Tags script
            Tags targetTags = hit.collider.GetComponent<Tags>();
            if (targetTags != null)
            {
                // Example interaction: Print the tagName from the Tags script
                Debug.Log("Interacted with: " + hit.collider.gameObject.name + ", Tag: " + targetTags.objectType);

                // Additional logic can be placed here
            }
            else
            {
                Debug.Log("No Tags component found on: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
