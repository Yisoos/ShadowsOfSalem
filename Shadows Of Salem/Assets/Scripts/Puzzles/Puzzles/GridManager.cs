using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public Vector2 spacing;

    [ContextMenu("Arrange objects in grid")]
    public void ArrangeObjectsInGrid()
    {
        List<Transform> childTransforms = GetChildTransforms(transform);

        // Dynamically calculate rows or columns if needed
        if (columns == 0) columns = Mathf.CeilToInt((float)childTransforms.Count / rows);
        if (rows == 0) rows = Mathf.CeilToInt((float)childTransforms.Count / columns);

        // Calculate the total size of the grid
        float totalWidth = (columns - 1) * spacing.x;  // Total width of the grid
        float totalHeight = (rows - 1) * spacing.y;    // Total height of the grid

        Vector3 gridCenterOffset = new Vector3(totalWidth / 2, -totalHeight / 2, 0); // Offset to center the grid

        for (int i = 0; i < childTransforms.Count; i++)
        {
            int row = i / columns;
            int column = i % columns;

            // Calculate the target position relative to the grid center
            Vector3 targetPosition = new Vector3(column * spacing.x, -row * spacing.y, 0) - gridCenterOffset;

            // Apply the position relative to the parent
            childTransforms[i].position = transform.position + targetPosition;
        }
    }

    List<Transform> GetChildTransforms(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        for (int i = 0; i < parent.childCount; i++)
        {
            children.Add(parent.GetChild(i));
        }

        return children;
    }
}
