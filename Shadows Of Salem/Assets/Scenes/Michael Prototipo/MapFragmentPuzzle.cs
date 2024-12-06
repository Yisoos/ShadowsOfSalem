using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFragmentPuzzle : MonoBehaviour
{
    public GameObject[] mapFragments; // Array de fragmentos del mapa
    public GameObject finalMap; // Objeto del mapa final
    private bool[] fragmentCollected;

    void Start()
    {
        fragmentCollected = new bool[mapFragments.Length];

        // Inicializar los fragmentos del mapa
        foreach (GameObject fragment in mapFragments)
        {

        }

        finalMap.SetActive(false); // Asegurarse de que el mapa final esté oculto al inicio
    }

    void Update()
    {
        // Detectar clic en un fragmento del mapa
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedFragment = hit.transform.gameObject;
                CollectFragment(clickedFragment);
            }
        }
    }

    void CollectFragment(GameObject fragment)
    {
        for (int i = 0; i < mapFragments.Length; i++)
        {
            if (fragment == mapFragments[i])
            {
                fragmentCollected[i] = true;
                fragment.SetActive(false); // Desactivar el fragmento recogido
                break;
            }
        }

        // Verificar si todos los fragmentos han sido recogidos
        if (AllFragmentsCollected())
        {
            StartCoroutine(CompleteMap());
        }
    }

    bool AllFragmentsCollected()
    {
        foreach (bool collected in fragmentCollected)
        {
            if (!collected)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator CompleteMap()
    {

        Debug.Log("¡Todos los fragmentos del mapa han sido recogidos!");
        finalMap.SetActive(true); // Mostrar el mapa final
        yield return null;
    }
}
