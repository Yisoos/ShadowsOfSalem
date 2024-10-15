using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CajonControlador : MonoBehaviour
{
    public GameObject panelCajonCerrado; // Asigna en el Inspector
    public GameObject panelCajonAbierto; // Asigna en el Inspector

    private void Start()
    {
        panelCajonCerrado.SetActive(true);
        panelCajonAbierto.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // Verifica si el objeto clickeado es el cajón
                if (hit.collider.CompareTag("Cajon"))
                {
                    TogglePanels();
                }
            }
        }
    }

    private void TogglePanels()
    {
        bool isCajonCerradoActive = panelCajonCerrado.activeSelf;
        panelCajonCerrado.SetActive(!isCajonCerradoActive);
        panelCajonAbierto.SetActive(isCajonCerradoActive);
    }

}
