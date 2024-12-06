using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatuePuzzle : MonoBehaviour
{
    public GameObject[] statues; // Array de estatuas en el orden correcto
    private int currentStatueIndex = 0;

    void Start()
    {
        // Inicializar el estado de las estatuas
        foreach (GameObject statue in statues)
        {

        }
    }

    void Update()
    {
        // Detectar clic en una estatua
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedStatue = hit.transform.gameObject;
                CheckStatue(clickedStatue);
            }
        }
    }

    void CheckStatue(GameObject statue)
    {
        if (statue == statues[currentStatueIndex])
        {
            // Estatua activada en el orden correcto
            currentStatueIndex++;
            

            if (currentStatueIndex >= statues.Length)
            {
                // Todas las estatuas han sido activadas en el orden correcto
                StartCoroutine(PuzzleSolved());
            }
        }
        else
        {
            // Orden incorrecto, restablecer el puzzle
            currentStatueIndex = 0;
           
        }
    }

    IEnumerator PuzzleSolved()
    {
        
        Debug.Log("¡Acertijo de estatuas resuelto!");
        yield return null;
    }
}
