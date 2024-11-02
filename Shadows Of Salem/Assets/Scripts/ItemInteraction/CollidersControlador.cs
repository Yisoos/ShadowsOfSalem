using System.Collections;
using UnityEngine;

public class CollidersControlador : MonoBehaviour
{
    // Variables públicas para configurar objetos y colliders desde el editor
    public GameObject objetoInteractivo; // Objeto interactivo que reacciona al clic
    public Collider2D[] colliders; // Array de colliders que representan objetos interactivos
    public GameObject[] activarObjetos; // Array de objetos a activar al hacer clic en colliders específicos
    ActivarPanel activarPanelScript; // Controlador de activación del zoomPanel
    public float colliderDelay = 0.5f; // Retraso antes de activar los colliders en zoom
    
    public FeedbackTextController feedbackTextController; // Controlador para mostrar mensajes de retroalimentación
    
    // Define los nombres de las capas en el orden de prioridad (de más alta a más baja)
    public string[] layerOrder = { "Prioritario","Default" };

    void Start()
    {
        activarPanelScript = GetComponent<ActivarPanel>();
        // Desactiva el panel de zoom y sus colliders al iniciar
        activarPanelScript.DeactivatePanel();
        ActivarColliders(false);
    }

    void Update()
    {
        // Detecta clic con botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Convierte la posición del ratón en coordenadas del mundo
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Lanza un rayo desde la posición del ratón para detectar colisiones
            RaycastHit2D hit = CheckLayersInOrder(mousePos);
            bool isAccessible = true;

            if (hit.collider != null)
            {
                isAccessible = ObjectAccessibilityChecker(hit.collider.gameObject);

                // Si el rayo choca con el objeto interactivo
                if (hit.collider.gameObject == objetoInteractivo && isAccessible)
                {
                    // Alterna la visibilidad del zoomPanel
                    activarPanelScript.TogglePanel();
                    // Desactiva temporalmente los colliders de los cajones
                    ActivarColliders(false);
                    // Activa los colliders tras un retraso de colliderDelay
                    StartCoroutine(EnableDrawerCollidersWithDelay());
                }
            }

            // Recorre todos los colliders para verificar interacciones
            for (int i = 0; i < colliders.Length; i++)
            {

                if (hit.collider != null)
                {
                    // Si el collider clickeado coincide con uno del array
                    if (hit.collider.gameObject == colliders[i].gameObject)
                    {
                        // Si el objeto es accesible, activa o desactiva el objeto correspondiente
                        if (isAccessible)
                        {
                            GameObject objectToToggle = activarObjetos[i];
                            objectToToggle.SetActive(!objectToToggle.activeSelf); // Alterna el estado activo del objeto
                        }
                    }
                }
            }
        }
    }

    // Método para verificar si el objeto golpeado es accesible
    bool ObjectAccessibilityChecker(GameObject objectHit)
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Lock itemLocked = objectHit.GetComponent<Lock>();
        Tags itemTags = objectHit.GetComponent<Tags>();
        DependencyHandler itemDependencies = objectHit.GetComponent<DependencyHandler>();
        CombinationLockControl combinationLocked = objectHit.GetComponent<CombinationLockControl>();
        Coleccionable collectable = objectHit.GetComponent<Coleccionable>();

        // Si el objeto está bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null && itemLocked.isLocked)
        {
            feedbackTextController.PopUpText(itemTags.displayText);
            return false;
        }
        // Si el objeto tiene dependencias no cumplidas, muestra mensaje y devuelve falso
        if (itemDependencies != null && !itemDependencies.dependencyMet)
        {
            feedbackTextController.PopUpText(itemTags.displayText);
            return false;
        }
        // Si el objeto está en un candado de combinación, muestra mensaje y devuelve falso
        if (combinationLocked != null && combinationLocked.isLocked)
        {
           
            return false;
        }
        // Si el objeto es coleccionable, lo recoge y devuelve falso
        if (collectable != null)
        {
            collectable.CollectItem();
            return false;
        }

        // Si no hay restricciones, devuelve verdadero
        return true;
    }

    // Método que lanza un raycast en el orden de capas especificado
    RaycastHit2D CheckLayersInOrder(Vector2 origin)
    {
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



    // Activa o desactiva todos los colliders del array
    void ActivarColliders(bool isActive)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isActive; // Cambia el estado de cada collider
        }
    }

    // Corrutina para activar los colliders tras un retraso
    IEnumerator EnableDrawerCollidersWithDelay()
    {
        yield return new WaitForSeconds(colliderDelay);
        ActivarColliders(true); // Activa los colliders después del retraso
    }
}
