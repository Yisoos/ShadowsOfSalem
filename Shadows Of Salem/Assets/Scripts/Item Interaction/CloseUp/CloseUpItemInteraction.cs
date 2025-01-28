using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpItemInteraction : MonoBehaviour
{
    public ColliderSets[] objetosInteractuables;

    private void OnEnable()
    {
        for (int i = objetosInteractuables.Length-1; i >= 0; i--)
        {
            if (objetosInteractuables[i].estadoAlternativo.gameObject.activeSelf)
            {
                ToggleObjectState(i, false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused) return; ; // Ignorar cualquier input del usuario cuando el juego está en pausa

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Lanza un rayo desde la posición del ratón para detectar colisiones
            RaycastHit2D hit = CheckLayersInOrder(mousePos);

            if (hit.collider != null && AccesibilityChecker.Instance.ObjectAccessibilityChecker(hit.transform))
            {
                ObjectDetector(hit.collider);
            }
        }
        
    }
    public void ObjectDetector(Collider2D objectCollider)
    {
                for (int i = 0; i < objetosInteractuables.Length; i++)
                {
                    if (objectCollider == objetosInteractuables[i].estadoInicial)
                    {
                        ToggleObjectState(i, true);
                        break;
                    }
                    else if (objectCollider == objetosInteractuables[i].estadoAlternativo)
                    {  
                            ToggleObjectState(i, false);   
                    }
                }
    }
    public void ToggleObjectState(int index, bool initialState) 
    {
        if (objetosInteractuables[index].estadoInicial != objetosInteractuables[index].estadoAlternativo)
        {
            if (initialState)
            {
                objetosInteractuables[index].estadoInicial.gameObject.SetActive(false);
                objetosInteractuables[index].estadoAlternativo.gameObject.SetActive(true);
                //Debug.Log("Objeto está en su estado inicial, cambiando...");
            }
            if (!initialState)
            {
                objetosInteractuables[index].estadoInicial.gameObject.SetActive(true);
                objetosInteractuables[index].estadoAlternativo.gameObject.SetActive(false);
                //Debug.Log("Objeto está en estado alternativo, cambiando...");
            }
        }
        else
        {
            //Debug.Log("Estado inicial es igual al estado Alternativo, no hay cambio");
        }
    }
    RaycastHit2D CheckLayersInOrder(Vector2 origin)
    {
        string[] layerOrder = {"Prioritario","Default"};
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
