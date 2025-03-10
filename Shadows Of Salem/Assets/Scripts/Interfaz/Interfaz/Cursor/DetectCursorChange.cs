﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectCursorChange : MonoBehaviour
{
    public List<Collider2D> cursorChangingItems; // List of colliders where cursor should change
    public int cursorIndex = 0; // Index of the cursor variant to show

    void Start()
    {
        // Optionally initialize something here
    }

    void Update()
    {
        if (PauseMenu.isPaused) return; ; // Ignorar cualquier input del usuario cuando el juego está en pausa

        if (CursorChanger.instance != null)
        {
            // Get the mouse position in world space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Perform a raycast at the mouse position
            RaycastHit2D hit = CursorChanger.instance.CheckLayersInOrder(mousePosition);

            if (hit.collider != null)
            {
                if (cursorChangingItems.Contains(hit.collider))
                {
                    if (Input.GetMouseButton(0))
                    {
                        CursorChanger.instance.ChangeCursor(cursorIndex + 1); // Change to the next cursor variant
                    }
                    else
                    {
                        CursorChanger.instance.ChangeCursor(cursorIndex); // Reset cursor when the left mouse button is not held down
                    }
                }
                else 
                {
                    CursorChanger.instance.SetCursorToDefault();
                }
            }
            // Check if the hit collider is in the list of items that should change the cursor
            else 
            {
                CursorChanger.instance.SetCursorToDefault();
            }       
        }
    }
    [ContextMenu("Anadir todos los Coliders")]
    private void PopulateColliders()
    {
        cursorChangingItems.Clear();
        // Find all Collider2D components in the scene
        cursorChangingItems.AddRange(Resources.FindObjectsOfTypeAll<Collider2D>());
    }
}

