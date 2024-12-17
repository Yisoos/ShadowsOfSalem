using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UIButtonCursorChanger))]
// Clase que maneja la mecánica de arrastre de un objeto en la interfaz de usuario
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Declaración de Clase y Variables
    [SerializeField] public NewTags tagInfo; 
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
        // Restaurar el cursor a la normalidad
        CursorChanger.instance.SetCursorToDefault();

        // Restablecer la escala del objeto
        transform.localScale = Vector3.one;

        //Debug.Log(thisTag.objectName);

        // Devolver el objeto a su padre original después de arrastrarlo
        transform.SetParent(parentAfterDrag);

        // Llamar al método para comprobar interacciones con otros objetos
        CheckForInteraction(eventData, tagInfo);

        // Rehabilitar el raycast en la imagen arrastrada
        image.raycastTarget = true;
    }
    #endregion

    #region Verificación y Manejo de Interacciones (CheckForInteraction, InteractionHub)
    // Método para verificar interacciones al soltar el objeto
    public void CheckForInteraction(PointerEventData eventData, NewTags thisObjectTag)
    {
        // Lanzar un rayo desde la posición del ratón en 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Rayo con dirección cero para obtener el objeto debajo del ratón

        // Comprobar si el rayo golpeó algo
        if (hit.collider != null)
        {
            // Obtener el componente de Tags del objeto golpeado
            InventoryItem targetTag = hit.collider.GetComponent<InventoryItem>();
            if (targetTag != null)
            {
                // Lógica adicional puede ser colocada aquí
                InteractionHub(this, targetTag);
            }
            else
            {
                //Debug.Log("No se encontró el componente Tags en: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            //Debug.Log("El raycast no golpeó nada.");
        }
    }

    // Método para manejar las interacciones basadas en el tipo de objeto objetivo
    public void InteractionHub(InventoryItem thisObjectTags, InventoryItem targetObjectTags)
    {
        // Manejar interacciones basadas en el tipo de objeto
        switch (targetObjectTags.tagInfo.objectType)
        {
            case TypeObject.Lock:
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

            case TypeObject.Compartment:
                // Manejar compartimentos bloqueados y dependencias
                Lock compartmentLocked = targetObjectTags.GetComponent<Lock>();
                DependencyHandler compartmentDependency = targetObjectTags.GetComponent<DependencyHandler>();
                OrderedDependencies compartmentDependencyInOrder = targetObjectTags.GetComponent<OrderedDependencies>();
                ObjectCombination compartmentObjectCombination = targetObjectTags.GetComponent<ObjectCombination>();
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
                if (compartmentDependencyInOrder != null)
                {
                    compartmentDependencyInOrder.HandleItem(thisObjectTags);
                }
                if (compartmentObjectCombination != null)
                {
                    compartmentObjectCombination.CheckForCombination(thisObjectTags);
                }
                break;

            default:
                // Manejar dependencias para otros tipos de objetos
                DependencyHandler dependency = targetObjectTags.GetComponent<DependencyHandler>();
                OrderedDependencies dependencyInOrder = targetObjectTags.GetComponent<OrderedDependencies>();
                ObjectCombination objectCombination = targetObjectTags.GetComponent<ObjectCombination>();
                InterchangableItemPlacement interchangableItemPlacement = targetObjectTags.GetComponent<InterchangableItemPlacement>();
                if (dependency != null)
                {
                    dependency.HandleItem(thisObjectTags);
                }
                if (dependencyInOrder != null)
                {
                    dependencyInOrder.HandleItem(thisObjectTags);
                }
                if (objectCombination != null)
                {
                    objectCombination.CheckForCombination(thisObjectTags);
                }
                if (interchangableItemPlacement != null)
                {
                    interchangableItemPlacement.PlaceOrSwapItem(thisObjectTags);
                }
                break;

        }
    }
    #endregion
}
