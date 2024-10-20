using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersControlador : MonoBehaviour
{
    // un array que guarda los colliders 
    public Collider2D[] colliders;

    // un array que guarda todo los objetos que queremos activar
    public GameObject[] activarObjetos;

    public GameObject panel;

    void Update()
    {
        // detectar click con el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonDown(0) && panel.activeInHierarchy)
        {
            // obtener la posici�n actual del rat�n en la pantalla y la convertimos a coordenadas del mundo
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // lanzar un rayo desde la posici�n del rat�n
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // indica la direcci�n en la que se lanza el rayo, pero como queremos detectar colisiones justo debajo del rat�n, lo mantenemos en Vector2.zero
            Debug.Log("Clic detectado");


            // en el caso de que el rayo choca con un collider:
            if (hit.collider != null)
            {
                // recorrer los colliders para encontrar cu�l fue clickeado
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
    }

}
