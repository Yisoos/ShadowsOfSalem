using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRaycast : MonoBehaviour
{

    public float raycastDistance = 10f;  // Distancia del rayo
    public LayerMask selfLayerMask;      // La capa que detecta el raycast (la capa de este objeto)

    private void Start()
    {
        // Asegúrate de que el objeto al que pertenece este script esté en la capa `SelfLayer`
        gameObject.layer = LayerMask.NameToLayer("SelfLayer");
    }

    private void Update()
    {
        // Crear el rayo que sale desde la posición del objeto y apunta hacia adelante
        Ray ray = new Ray(transform.position, transform.forward);

        // Realizar el Raycast con la capa de solo detectar el propio objeto
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, selfLayerMask))
        {
            // Si el rayo colisiona con el objeto, este es el collider de su propio objeto
            if (hit.collider.gameObject == gameObject)
            {
                Debug.Log("El rayo ha golpeado su propio objeto: " + hit.collider.gameObject.name);
            }
        }

        // Dibuja el rayo en la vista de la escena para depuración
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
    }
}
