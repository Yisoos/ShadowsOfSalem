using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRearranging : MonoBehaviour
{
    GridManager grid; // Reference to a Grid component in the parent
    private Vector3 offset; // Offset to maintain proper positioning while dragging
    private Transform parentTransform; // Reference to the parent of this object

    private OrderMechanic orderMechanic;
    private void Start()
    {
        grid = GetComponentInParent<GridManager>();
        parentTransform = transform.parent;
        orderMechanic = parentTransform.GetComponent<OrderMechanic>();

    }

    // Called when the mouse button is pressed down on the object
    public void OnMouseDown()
    {
        // Calculate offset between mouse position and object position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, transform.position.y, transform.position.z);
    }

    // Called while the object is being dragged
    public void OnMouseDrag()
    {
        // Get mouse position in world space and update object's position on the x-axis
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x + offset.x, transform.position.y, transform.position.z);
    }

    // Called when the mouse button is released
    public void OnMouseUp()
    {
        if (parentTransform == null)
            return;

        // Find the first valid sibling hit
        Transform hitTransform = GetSiblingAtPosition(transform.position);

        if (hitTransform != null)
        {
            // Swap hierarchy order
            int currentIndex = transform.GetSiblingIndex();
            int hitIndex = hitTransform.GetSiblingIndex();

            transform.SetSiblingIndex(hitIndex);
            hitTransform.SetSiblingIndex(currentIndex);
        }

        // Rearrange objects in the grid if the grid exists
        if (grid != null)
        {
            grid.ArrangeObjectsInGrid();
        }

        if (orderMechanic != null)
        {
            orderMechanic.UpdateOrder();
        }
    }

    // Function to get the first valid sibling at a given position
    private Transform GetSiblingAtPosition(Vector3 position)
    {
        // Get all colliders at the position
        Collider2D[] hits = Physics2D.OverlapPointAll(position);

        foreach (var hit in hits)
        {
            // Skip if the hit collider belongs to the current object
            if (hit.transform == transform)
                continue;

            // Skip if the hit object is not a sibling
            if (hit.transform.parent != parentTransform)
                continue;

            // Return the first valid sibling
            return hit.transform;
        }

        // Return null if no valid sibling is found
        return null;
    }
}
