using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D customCursor;    // Custom cursor texture
    public Texture2D defaultCursor;   // Default cursor texture
    public Vector2 cursorHotspot = Vector2.zero; // Offset for cursor position, usually center of the texture

    private Camera mainCamera;
    private bool isCustomCursorActive = false;

    void Start()
    {
        mainCamera = Camera.main;
        Debug.Log("CursorChanger initialized for 2D.");

        // Check if camera is correctly set
        if (mainCamera != null)
        {
            Debug.Log("Main camera found.");
            Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
            Debug.Log("Default cursor set.");
        }
        else
        {
            Debug.LogError("Main camera not found! Ensure there is a camera tagged as MainCamera.");
        }
    }

    void Update()
    {
        if (mainCamera == null) return; // Safety check

        // Perform a 2D raycast from the camera to the mouse position
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Cursor over object: " + hit.collider.gameObject.name);

            // Change cursor to custom cursor if over an interactable collider
            if (!isCustomCursorActive)
            {
                Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
                isCustomCursorActive = true;
                Debug.Log("Custom cursor activated.");
            }
        }
        else
        {
            ResetCursor();
        }
    }

    private void ResetCursor()
    {
        if (isCustomCursorActive)
        {
            Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
            isCustomCursorActive = false;
            Debug.Log("Default cursor reactivated.");
        }
    }
}
