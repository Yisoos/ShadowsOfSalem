using System.Collections;
using UnityEngine;

public class CollidersControlador : MonoBehaviour
{
    // Variables públicas para configurar objetos y colliders desde el editor
    public GameObject objetoInteractivo; // Objeto interactivo que reacciona al clic
    public Collider2D[] colliders; // Array de colliders que representan objetos interactivos
    public GameObject[] activarObjetos; // Array de objetos a activar al hacer clic en colliders específicos
    public GameObject zoomPanel; // Panel de zoom, se activa al interactuar con objetoInteractivo
    public ActivarPanel activarZoomPanel; // Controlador de activación del zoomPanel
    public float colliderDelay = 0.5f; // Retraso antes de activar los colliders en zoom
    public FeedbackTextController feedbackTextController; // Controlador para mostrar mensajes de retroalimentación
    // Define los nombres de las capas en el orden de prioridad (de más alta a más baja)
    public string[] layerOrder = { "Prioritario","Default" };

    void Start()
    {
        // Desactiva el panel de zoom y sus colliders al iniciar
        zoomPanel.SetActive(false);
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
                    activarZoomPanel.TogglePanel();
                    // Desactiva temporalmente los colliders de los cajones
                    ActivarColliders(false);
                    // Activa los colliders tras un retraso de colliderDelay
                    StartCoroutine(EnableDrawerCollidersWithDelay());
                }
            }

            // Recorre todos los colliders para verificar interacciones
            for (int i = 0; i < colliders.Length; i++)
            {
               

                // Si el collider clickeado coincide con uno del array
                if (hit.collider == colliders[i])
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
        foreach (string layerName in layerOrder)
        {
            // Crear un LayerMask para la capa actual
            int layerMask = LayerMask.GetMask(layerName);

            // Lanzar el raycast en la capa especificada
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                return hit; // Devuelve el primer hit encontrado en el orden de prioridad
            }
        }
        return new RaycastHit2D(); // Devuelve un raycast vacío si no se encontró ningún collider
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
