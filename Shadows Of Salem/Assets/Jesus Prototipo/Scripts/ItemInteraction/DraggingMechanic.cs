using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggingMechanic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;  // Disable raycast on dragged object
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
        Tags thisTag = GetComponent<Tags>();
        Debug.Log("Dropped Object: " + gameObject.name + ", Tag: " + thisTag.objectType);

        // Check if we are dropping over a UI element (like inventory)
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            CheckForInteraction(eventData, thisTag);
        }

        // Snap back to the original parent after dragging
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;  // Re-enable raycast on dragged object
    }

    public void CheckForInteraction(PointerEventData eventData, Tags thisObjectTag)
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
                // Log information about the object being dropped onto
                Debug.Log("Dropped onto: " + hit.collider.gameObject.name + ", Tag: " + targetTags.objectType);

                // Additional logic can be placed here
                InteractionHub(targetTags, thisObjectTag);

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
    public void InteractionHub(Tags targetObjectTags, Tags thisObjectTags)
    {
        switch (targetObjectTags.objectType)
        {
            case ObjectType.Lock:
                Lock lockComponent = targetObjectTags.GetComponent<Lock>();
                if (lockComponent != null)
                {
                    Key key = GetComponent<Key>();
                    if (key != null)
                    {
                        lockComponent.TryUnlock(key);
                    }
                }
                break;
            case ObjectType.Compartment:
                Lock compartmentLocked = targetObjectTags.GetComponent<Lock>();
                DependencyHandler compartmentDependency = targetObjectTags.GetComponent<DependencyHandler>();
                if (compartmentLocked != null&&compartmentLocked.isLocked==true)
                {
                    Key key = GetComponent<Key>();
                    if (key != null)
                    {
                        compartmentLocked.TryUnlock(key);
                    }
                }
                else if (compartmentDependency != null)
                {
                    bool dependenciesMet = compartmentDependency.HandleItem(thisObjectTags);
                }
                break;
            default:
                DependencyHandler dependency = targetObjectTags.GetComponent<DependencyHandler>();
                if (dependency != null) 
                {
                    bool dependenciesMet = dependency.HandleItem(thisObjectTags);
                }
                break;
        }
    }
}
