using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CollidersControlador : MonoBehaviour
{
    public GameObject objetoInteractivo;
    public Collider2D[] colliders; // un array que guarda los colliders 
    public GameObject[] activarObjetos; // un array que guarda todo los objetos que queremos activar
    public GameObject zoomPanel; // el zoom
    public ActivarPanel activarZoomPanel; 
    public float colliderDelay = 0.5f; // retraso antes de activar los colliders de los cajones

    void Start()
    {
        activarZoomPanel = FindAnyObjectByType<ActivarPanel>();
        // desactivar el zoom y sus colliders 
        zoomPanel.SetActive(false);
        ActivarColliders(false);
    }

    void Update()
    {

        // detectar click con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))

        {
            // obtener la posición actual del ratón en la pantalla y la convertimos a coordenadas del mundo
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // lanzar un rayo desde la posición del ratón
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // indica la dirección en la que se lanza el rayo, pero como queremos detectar colisiones justo debajo del ratón, lo mantenemos en Vector2.zero
            Debug.Log("Clic detectado");


            // en el caso de que el rayo choca con el collider del objeto interactivo:
            if (hit.collider != null && hit.collider.gameObject == objetoInteractivo)
            {
                //zoomPanel.SetActive(true);
                activarZoomPanel.TogglePanel();
                ActivarColliders(false);

                StartCoroutine(EnableDrawerCollidersWithDelay()); // activamos los colliders tras el retraso de .5 segundos

            }
            // recorrer los colliders para encontrar cuál fue clickeado
            for (int i = 0; i < colliders.Length; i++)
            {
                // ver si el collider clickeado coincide con uno de los colliders puestos 
                if (hit.collider == colliders[i])
                {
                    // activar el game object que corresponda
                    GameObject objectToToggle = activarObjetos[i];
                    objectToToggle.SetActive(!objectToToggle.activeSelf); // chequea si el game object esta activado o no; si esta activado, lo desactiva y si esta desactivado, lo activa
                    Debug.Log("Activar " + colliders[i]);
                }
            }
        }
    }

    void ActivarColliders(bool isActive)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isActive; // desactivamos todos los colliders en el zoom
        }
    }

    // los volvemos a activar tras el retraso
    IEnumerator EnableDrawerCollidersWithDelay()
    {
        yield return new WaitForSeconds(colliderDelay); 
        ActivarColliders(true); 
    }

}
