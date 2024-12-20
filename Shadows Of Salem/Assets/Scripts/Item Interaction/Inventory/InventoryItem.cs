using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UIButtonCursorChanger))]
// Clase que maneja la mecánica de arrastre de un objeto en la interfaz de usuario
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    public Tags itemTag; 
    // Imagen que representa el objeto que se arrastra
    public Image image;
    public float magnificationOnDrag;
    // Transformación del padre del objeto después de ser arrastrado
    [HideInInspector] public Transform parentAfterDrag;
    
    // Método llamado al inicio del arrastre
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guardar el padre original del objeto
        parentAfterDrag = transform.parent;

        // Cambiar el padre a la raíz para que flote en la interfaz
        transform.SetParent(transform.root);

        // Mover el objeto al final de la lista de hermanos en la jerarquía
        transform.SetAsLastSibling();

        TMP_Text itemQuantityText = GetComponentInChildren<TMP_Text>();
        if (itemQuantityText != null)
        {
            itemQuantityText.color = new Color(itemQuantityText.color.r, itemQuantityText.color.g, itemQuantityText.color.b, 0);
        }

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
        CheckForInteraction(eventData, itemTag);

        TMP_Text itemQuantityText = GetComponentInChildren<TMP_Text>();
        if (itemQuantityText != null)
        {
            itemQuantityText.color = new Color(itemQuantityText.color.r, itemQuantityText.color.g, itemQuantityText.color.b, 255);
        }

            // Rehabilitar el raycast en la imagen arrastrada
            image.raycastTarget = true;
    }
    // Método para verificar interacciones al soltar el objeto
    public void CheckForInteraction(PointerEventData eventData, Tags thisObjectTag)
    {
        // Lanzar un rayo desde la posición del ratón en 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Rayo con dirección cero para obtener el objeto debajo del ratón

        // Comprobar si el rayo golpeó algo
        if (hit.collider != null)
        {
                AccesibilityChecker.Instance.TryAccess(this, hit.collider.transform);
        }
        else
        {
            //Debug.Log("El raycast no golpeó nada.");
        }
    }
}
