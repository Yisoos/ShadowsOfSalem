 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndSwapMechanic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform[] objectsToSort; // Los objetos que queremos ordenar
    public FeedbackTextController feedbackText; // Texto para retroalimentación

    private Transform targetObject; // Objeto sobre el que se soltó
    private Vector3 startPosition; // Posición inicial del objeto arrastrado
    //private Transform startParent; // Padre original del objeto arrastrado
    //public Texture2D dragCursor; // Cursor personalizado para arrastrar
    //public Vector2 cursorHotspot = Vector2.zero; // Punto caliente del cursor


    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guardar la posición inicial y el padre del objeto
        startPosition = transform.position;
       // startParent = transform.parent;

        // Cambiar el cursor al cursor personalizado
        //Cursor.SetCursor(dragCursor, cursorHotspot, CursorMode.Auto);

        // Mover el objeto al final de la jerarquía para que esté encima de otros
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Seguir la posición del cursor del ratón
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Mantener el objeto en el plano 2D
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaurar el cursor predeterminado
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        // Verificar si se soltó sobre otro objeto
        targetObject = GetObjectUnderCursor();

        if (targetObject != null && targetObject != transform)
        {
            // Intercambiar posiciones entre este objeto y el objetivo
            Vector3 targetPosition = targetObject.position;
            targetObject.position = startPosition;
            transform.position = targetPosition;

            // Opcional: Imprimir los cambios
            Debug.Log($"Intercambiado con {targetObject.name}");
        }
        else
        {
            // Volver a la posición inicial si no se soltó sobre un objeto válido
            transform.position = startPosition;
        }

        // Restaurar al padre original
       // transform.SetParent(startParent);

        // Verificar si los objetos están en el orden correcto
        CheckCorrectOrder();
    }

    private Transform GetObjectUnderCursor()
    {
        // Detectar el objeto bajo el cursor
        foreach (Transform obj in objectsToSort)
        {
            if (obj != transform) // No comparar consigo mismo
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                if (Vector2.Distance(mousePosition, obj.position) < 0.5f) // Ajustar distancia según tamaño de los sprites
                {
                    return obj;
                }
            }
        }

        return null;
    }

    private void CheckCorrectOrder()
    {
        // Verificar si los objetos están en orden correcto (basado en su posición en la jerarquía)
        bool isCorrect = true;

        for (int i = 0; i < objectsToSort.Length - 1; i++)
        {
            if (objectsToSort[i].position.x > objectsToSort[i + 1].position.x)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect && feedbackText != null)
        {
            // Mostrar retroalimentación
            feedbackText.PopUpText("Ese sonido otra vez. ¿Qué he activado?");
        }
       
    }
}
