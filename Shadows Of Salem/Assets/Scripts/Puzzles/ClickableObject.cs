using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // Referencia al PuzzleSequenceManager
    public PuzzleSequenceManager puzzleManager;

    // Función que detecta el clic sobre el objeto
    private void OnMouseDown()
    {
        if (puzzleManager != null)
        {
            // Notificamos al PuzzleManager que se hizo clic en este objeto
            puzzleManager.OnObjectClicked(gameObject);
        }
    }
}
