using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab; // Prefab del fantasma a generar
    public Transform[] spawnPoints; // Puntos de generaci�n posibles
    public float spawnInterval = 5f; // Intervalo de tiempo entre cada generaci�n

    void Start()
    {
        // Iniciar la corrutina para generar fantasmas
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo especificado antes de generar el pr�ximo fantasma
            yield return new WaitForSeconds(spawnInterval);

            // Elegir un punto de generaci�n aleatorio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Generar el fantasma en el punto seleccionado
            Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);

            
        }
    }
}
