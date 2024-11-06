using UnityEngine;
using UnityEngine.EventSystems; // Requerido para detectar eventos de UI

public class CursorChanger : MonoBehaviour
{
    [HideInInspector] public static CursorChanger instance;
    public Texture2D[] variantesCursor;      // Texturas de cursor personalizadas
    public Texture2D cursorPorDefecto;       // Textura del cursor por defecto
    public Vector2 cursorHotspot = Vector2.zero;

    private bool isDefaultCursorActive;       // Verifica si el cursor por defecto est� activo
    private bool isOnUIButton = false;         // Indica si el cursor est� sobre un bot�n de UI
    private float resetTimer = 0f;             // Temporizador para resetear el cursor
    private float cursorResetDelay = 0.1f;     // Retraso antes de restablecer el cursor

    private void Awake()
    {
        // Configura esta instancia como la �nica del CursorChanger
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // Destruye si ya existe otra instancia
        }
    }

    void Start()
    {
        SetCursorToDefault(); // Establece el cursor por defecto al inicio
    }

    void Update()
    {
        // Evita la detecci�n de colisiones si el cursor est� sobre un bot�n de UI
        if (isOnUIButton)
        {
            resetTimer -= Time.deltaTime;
            return;
        }

        // Detecta si el cursor est� sobre un colisionador
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = CheckLayersInOrder(mousePosition);

        if (hit.collider != null && hit.collider.gameObject.tag!="Open Container")
        {
            ChangeCursor(0); // Cambia al cursor personalizado si hay un colisionador
        }
        else
        {
            SetCursorToDefault(); // Restablece el cursor al predeterminado si no hay colisionador
        }
    }

    public void ChangeCursor(int cursorIndex)
    {
        if (cursorIndex < variantesCursor.Length)
        {
            // Cambia el cursor seg�n el �ndice especificado en el arreglo de variantes
            cursorHotspot = new Vector2(variantesCursor[cursorIndex].width / 2, variantesCursor[cursorIndex].width / 2);
            Cursor.SetCursor(variantesCursor[cursorIndex], cursorHotspot, CursorMode.Auto);
            isDefaultCursorActive = false;
            resetTimer = cursorResetDelay; // Activa el temporizador para resetear el cursor despu�s de un tiempo
        }
    }

    public void SetCursorToDefault()
    {
        // Configura el cursor al cursor por defecto solo si no est� activo
        if (!isDefaultCursorActive)
        {
            cursorHotspot = new Vector2(8, 8);
            Cursor.SetCursor(cursorPorDefecto, cursorHotspot, CursorMode.Auto);
            isDefaultCursorActive = true;
        }
    }

    // Llamado cuando el cursor entra en un bot�n de UI
    public void SetCursorUI(int cursorIndex)
    {
        ChangeCursor(cursorIndex); // Cambia el cursor al �ndice especificado para UI
        isOnUIButton = true;
    }

    // Llamado cuando el cursor sale de un bot�n de UI
    public void ResetCursorUI()
    {
        SetCursorToDefault(); // Restablece el cursor al por defecto
        isOnUIButton = false;
    }
    RaycastHit2D CheckLayersInOrder(Vector2 origin)
    {
        string[] layerOrder = { "Prioritario", "Default" };
        // Raycast all objects at the origin position with any layer.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.zero, Mathf.Infinity);

        if (hits.Length > 0)
        {
            // Convert the first layer name in layerOrder to a LayerMask integer
            int topPriorityLayer = LayerMask.NameToLayer(layerOrder[0]);

            // Check if any hit belongs to the top-priority layer
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.layer == topPriorityLayer)
                {
                    // Return the first object found in the top-priority layer
                    return hit;
                }
            }

            // If no objects from the top-priority layer were found, return the first hit
            return hits[0];
        }

        // Return an empty RaycastHit2D if no objects were hit
        return new RaycastHit2D();
    }
}
