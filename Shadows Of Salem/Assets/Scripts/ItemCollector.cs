using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public Camera playerCamera; // Referencia a la c�mara del jugador
    public PlayerInventory playerInventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detectar clic izquierdo
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))  // Si el raycast toca un objeto
            {
                ItemWorld item = hit.collider.GetComponent<ItemWorld>();  // Aseg�rate de que el objeto tenga el script ItemWorld
                if (item != null)
                {
                    // Llamar al m�todo de agregar el �tem al inventario
                    playerInventory.AddItem(item);
                    Destroy(hit.collider.gameObject);  // Destruir el objeto despu�s de recogerlo
                }
            }
        }
    }
}
