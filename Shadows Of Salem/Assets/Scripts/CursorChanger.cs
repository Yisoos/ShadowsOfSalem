using UnityEngine;
using UnityEngine.EventSystems; // Required for detecting UI events

public class CursorChanger : MonoBehaviour
{
    [HideInInspector] public static CursorChanger instance;
    public Texture2D[] variantesCursor;      // Custom cursor textures
    public Texture2D cursorPorDefecto;       // Default cursor texture
    public Vector2 cursorHotspot = Vector2.zero;

    private bool isDefaultCursorActive = true;
    private bool externalCursorRequest = false;
    private bool isOnUIButton = false;       // Tracks if the cursor is over a UI button
    private float cursorResetDelay = 0.1f;
    private float resetTimer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SetCursorToDefault();
    }

    void Update()
    {
        // Skip collider detection if the cursor is over a UI button
        if (isOnUIButton || externalCursorRequest)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                externalCursorRequest = false;
            }
            return;
        }

        // Raycast to detect if cursor is over a collider
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            ChangeCursor(0);  // Change to custom cursor when hitting a collider
        }
        else
        {
            SetCursorToDefault();  // Reset to default cursor
        }
    }

    public void ChangeCursor(int cursorIndex)
    {
        if (cursorIndex < variantesCursor.Length)
        {
            Cursor.SetCursor(variantesCursor[cursorIndex], cursorHotspot, CursorMode.Auto);
            isDefaultCursorActive = false;
            externalCursorRequest = true;
            resetTimer = cursorResetDelay;
        }
    }

    public void ResetCursor()
    {
        SetCursorToDefault();
    }

    public void SetCursorToDefault()
    {
        if (!isDefaultCursorActive)
        {
            Cursor.SetCursor(cursorPorDefecto, cursorHotspot, CursorMode.Auto);
            isDefaultCursorActive = true;
        }
    }

    // Called when mouse enters a UI button
    public void SetCursorUI(int cursorIndex)
    {
        ChangeCursor(cursorIndex);
        isOnUIButton = true;
    }

    // Called when mouse exits a UI button
    public void ResetCursorUI()
    {
        SetCursorToDefault();
        isOnUIButton = false;
    }
}
