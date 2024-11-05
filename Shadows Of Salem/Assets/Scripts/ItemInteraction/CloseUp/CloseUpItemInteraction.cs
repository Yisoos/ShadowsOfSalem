using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpItemInteraction : MonoBehaviour
{
    public Collider2D[] objetosEstadoInicial;
    public Collider2D[] objetosEstadoAlternativo;
    public FeedbackTextController feedbackTextController;

    private void OnEnable()
    {
        for (int i = 0; i < objetosEstadoAlternativo.Length; ++i)
        {
            if (objetosEstadoAlternativo[i].gameObject.activeSelf)
            {
                ToggleObjectState(i, false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Lanza un rayo desde la posición del ratón para detectar colisiones
            RaycastHit2D hit = CheckLayersInOrder(mousePos);
            bool isAccesible= true;
            if (hit.collider != null) 
            { 
            isAccesible = ObjectAccessibilityChecker(hit.transform);
            }
            if (hit.transform != null && isAccesible)
            {
                int index = Array.IndexOf(objetosEstadoInicial, hit.collider);
               
                if (index != -1)
                {
                    ToggleObjectState(index, true);
                }
                else
                {
                    index = Array.IndexOf(objetosEstadoAlternativo, hit.collider);
                    if (index != -1)
                    {
                        ToggleObjectState(index, false);
                    }
                    else
                    {
                        Debug.Log("Objeto no está definido para interacción");
                    }
                }
                
            }
        }
    }

    public void ToggleObjectState(int index, bool initialState) 
    {
        if (objetosEstadoInicial[index] != objetosEstadoAlternativo[index])
        {
            if (initialState)
            {
                objetosEstadoInicial[index].gameObject.SetActive(false);
                objetosEstadoAlternativo[index].gameObject.SetActive(true);
                Debug.Log("Objeto está en su estado inicial, cambiando...");
            }
            if (!initialState)
            {
                objetosEstadoInicial[index].gameObject.SetActive(true);
                objetosEstadoAlternativo[index].gameObject.SetActive(false);
                Debug.Log("Objeto está en estado alternativo, cambiando...");
            }
        }
        else
        {
            Debug.Log("Estado inicial es igual al estado Alternativo, no hay cambio");
        }
    }

    bool ObjectAccessibilityChecker(Transform objectHit)
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
