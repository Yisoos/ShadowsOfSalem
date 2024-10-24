using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Clase que maneja la mec�nica de arrastre de un objeto en la interfaz de usuario
public class DraggingMechanic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Imagen que representa el objeto que se arrastra
    public Image image;

    // Transformaci�n del padre del objeto despu�s de ser arrastrado
    [HideInInspector] public Transform parentAfterDrag;

    // M�todo llamado al inicio del arrastre
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guardar el padre original del objeto
        parentAfterDrag = transform.parent;

        // Cambiar el padre a la ra�z para que flote en la interfaz
        transform.SetParent(transform.root);

        // Mover el objeto al final de la lista de hermanos en la jerarqu�a
        transform.SetAsLastSibling();

        // Desactivar el raycast en la imagen arrastrada
        image.raycastTarget = false;
    }

    // M�todo llamado mientras se arrastra el objeto
    public void OnDrag(PointerEventData eventData)
    {
        // Mover el objeto en la posici�n del rat�n
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ajustar Z a 0 para 2D
        transform.position = mousePos;
    }

    // M�todo llamado al final del arrastre
    public void OnEndDrag(PointerEventData eventData)
    {
        // Obtener el componente de Tags del objeto
        Tags thisTag = GetComponent<Tags>();

        // Devolver el objeto a su padre original despu�s de arrastrarlo
        transform.SetParent(parentAfterDrag);

        // Comprobar si se est� soltando sobre un elemento de la interfaz de usuario
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Llamar al m�todo para comprobar interacciones con otros objetos
            CheckForInteraction(eventData, thisTag);
        }

        // Rehabilitar el raycast en la imagen arrastrada
        image.raycastTarget = true;
    }

    // M�todo para verificar interacciones al soltar el objeto
    public void CheckForInteraction(PointerEventData eventData, Tags thisObjectTag)
    {
        // Lanzar un rayo desde la posici�n del rat�n en 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Lanzar un rayo con direcci�n cero para obtener el objeto debajo del rat�n

        // Comprobar si el rayo golpe� algo
        if (hit.collider != null)
        {
            // Obtener el componente de Tags del objeto golpeado
            Tags targetTags = hit.collider.GetComponent<Tags>();
            if (targetTags != null)
            {
                // L�gica adicional puede ser colocada aqu�
                InteractionHub(targetTags, thisObjectTag);
            }
            else
            {
                Debug.Log("No se encontr� el componente Tags en: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("El raycast no golpe� nada.");
        }
    }

    // M�todo para manejar las interacciones basadas en el tipo de objeto objetivo
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
                    bool dependenciesMet = compartmentDependency.HandleItem(thisObjectTags);
                }
                break;

            default:
                // Manejar dependencias para otros tipos de objetos
                DependencyHandler dependency = targetObjectTags.GetComponent<DependencyHandler>();
                if (dependency != null)
                {
                    bool dependenciesMet = dependency.HandleItem(thisObjectTags);
                }
                break;
        }
    }
}
