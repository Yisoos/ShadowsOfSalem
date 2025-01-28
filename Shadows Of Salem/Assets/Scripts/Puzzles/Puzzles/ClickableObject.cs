using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // Referencia al PuzzleSequenceManager
    public PuzzleSequenceManager puzzleManager;
    public GameObject indicator;

    private float duration = 0.2f;

    private void Start()
    {
        indicator.SetActive(false);
    }
    // Funci�n que detecta el clic sobre el objeto
    private void OnMouseDown()
    {
        if (PauseMenu.isPaused) return; ; // Ignorar cualquier input del usuario cuando el juego est� en pausa

        if (indicator != null && puzzleManager != null)
        {
            // Notificamos al PuzzleManager que se hizo clic en este objeto
            puzzleManager.OnObjectClicked(gameObject);
            StartCoroutine(ActivarPorTiempo());

        }
    }

    IEnumerator ActivarPorTiempo()
    {
        // Activar el GameObject
        indicator.SetActive(true);

        // Esperar la duraci�n especificada
        yield return new WaitForSeconds(duration);

        // Desactivar el GameObject despu�s de la espera
        indicator.SetActive(false);
    }
}
