using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#region Declaración de Clase y Variables
// Clase que maneja la mecánica de arrastre de un objeto en la interfaz de usuario
public class DraggingMechanic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Imagen que representa el objeto que se arrastra
    public Image image;
    public float magnificationOnDrag;

    // Transformación del padre del objeto después de ser arrastrado
    [HideInInspector] public Transform parentAfterDrag;
    #endregion

    #region Métodos de Arrastre (OnBeginDrag, OnDrag, OnEndDrag)
    // Método llamado al inicio del arrastre
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guardar el padre original del objeto
        parentAfterDrag = transform.parent;

        // Cambiar el padre a la raíz para que flote en la interfaz
        transform.SetParent(transform.root);

        // Mover el objeto al final de la lista de hermanos en la jerarquía
        transform.SetAsLastSibling();

        // Desactivar el raycast en la imagen arrastrada
        image.raycastTarget = false;
    }

    // Método llamado mientras se arrastra el objeto
    public void OnDrag(PointerEventData eventData)
    {
        // Cambiar el cursor al modo arrastre
        CursorChanger.instance.SetCursorUI(1);

        // Mover el objeto en la posición del ratón
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ajustar Z a 0 para 2D
        transform.position = mousePos;

        // Aplicar aumento durante el arrastre
        transform.localScale = Vector3.one * magnificationOnDrag;
    }

    // Método llamado al final del arrastre
    public void OnEndDrag(PointerEventData eventData)
    {
        // Restablecer la escala del objeto
        transform.localScale = Vector3.one;

        // Obtener el componente de Tags del objeto
        Tags thisTag = gameObject.GetComponent<Tags>();
        Debug.Log(thisTag.objectName);

        // Devolver el objeto a su padre original después de arrastrarlo
        transform.SetParent(parentAfterDrag);

        // Llamar al método para comprobar interacciones con otros objetos
        CheckForInteraction(eventData, thisTag);

        // Restaurar el cursor a la normalidad
        CursorChanger.instance.SetCursorUI(0);

        // Rehabilitar el raycast en la imagen arrastrada
        image.raycastTarget = true;
    }
    #endregion

    #region Verificación y Manejo de Interacciones (CheckForInteraction, InteractionHub)
    // Método para verificar interacciones al soltar el objeto
    public void CheckForInteraction(PointerEventData eventData, Tags thisObjectTag)
    {
        // Lanzar un rayo desde la posición del ratón en 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Rayo con dirección cero para obtener el objeto debajo del ratón

        // Comprobar si el rayo golpeó algo
        if (hit.collider != null)
        {
            // Obtener el componente de Tags del objeto golpeado
            Tags targetTags = hit.collider.GetComponent<Tags>();
            if (targetTags != null)
            {
                // Lógica adicional puede ser colocada aquí
                InteractionHub(targetTags, thisObjectTag);
            }
            else
            {
                Debug.Log("No se encontró el componente Tags en: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("El raycast no golpeó nada.");
        }
    }

    // Método para manejar las interacciones basadas en el tipo de objeto objetivo
    public void InteractionHub(Tags targetObjectTags, Tags thisObjectTags)
    {
        // Manejar interacciones basadas en el tipo de objeto
        switch (targetObjectTags.objectType)
        {
            case ObjectType.Lock:
                // Manejar desbloqueo si el objeto es un candado
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
                // Manejar compartimentos bloqueados y dependencias
                Lock compartmentLocked = targetObjectTags.GetComponent<Lock>();
                DependencyHandler compartmentDependency = targetObjectTags.GetComponent<DependencyHandler>();
                if (compartmentLocked != null && compartmentLocked.isLocked == true)
                {
                    Key key = GetComponent<Key>();
                    if (key != null)
                    {
                        compartmentLocked.TryUnlock(key);
                    }
                }
                else if (compartmentDependency != null)
                {
                    compartmentDependency.HandleItem(thisObjectTags);
                }
                break;

            default:
                // Manejar dependencias para otros tipos de objetos
                DependencyHandler dependency = targetObjectTags.GetComponent<DependencyHandler>();
                if (dependency != null)
                {
                    dependency.HandleItem(thisObjectTags);
                }
                break;
        }
    }
    #endregion
}
