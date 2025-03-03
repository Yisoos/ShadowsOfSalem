using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequenceManager : MonoBehaviour
{
    // Lista de los objetos en el orden correcto
    [Tooltip("Meter aqui los GameObjects en el orden que quereis. Recordar añadirles un collider.")]
    public GameObject[] sequenceObjects;

    //public GameObject openedBasement;
    [Header("When puzzle is solved:")]
    public Transform objectToMove;   
    public Vector3 newPosition;     

    // Almacena la secuencia de objetos clickeados por el jugador
    private GameObject[] playerClicks;
    private int clickCount = 0;

    // Feedback text para mostrar si es correcto o incorrecto
    public FeedbackTextController feedbackText;

    // Para evitar hacer clic mientras se procesan los clics
    private bool isPuzzleInProgress = false;

    // Collider Entrada Catacumbas
    public Collider2D entradaCatacumbas;

    private void Start()
    {
        //openedBasement.SetActive(false);
        // Asegurarse de que playerClicks esté inicializado correctamente
        playerClicks = new GameObject[5];
        entradaCatacumbas.enabled = false;
    }

    // Este método se llama cuando se hace clic en un objeto
    public void OnObjectClicked(GameObject clickedObject)
    {
        // Si ya estamos procesando la secuencia, no hacer nada
        if (isPuzzleInProgress)
            return;

        // Registrar el clic del jugador
        playerClicks[clickCount] = clickedObject;
        clickCount++;


        // Si ya se han registrado 4 clics, verificar si la secuencia es correcta
        if (clickCount >= 4)
        {
            // Iniciar el proceso de validación
            isPuzzleInProgress = true;

            // Verificar si la secuencia es correcta
            if (IsSequenceCorrect())
            {
                Debug.Log("¡Puzzle completado!");
                //openedBasement.SetActive(true);
                MoveObject();
                entradaCatacumbas.enabled = false;

                feedbackText.PopUpText("¿Qué fue ese sonido? Viene del pasillo.");

            }
            else
            {
                Debug.Log("¡Incorrecto! Intenta de nuevo.");
                feedbackText.PopUpText("¡Uy! Ese no es.");

                // Reiniciar el puzzle después de 1 segundo
                Invoke("RestartPuzzle", 1f);
            }
        }
    }

    // Verifica si la secuencia de clics es correcta
    private bool IsSequenceCorrect()
    {
        for (int i = 0; i < sequenceObjects.Length; i++)
        {
            if (playerClicks[i] != sequenceObjects[i])
            {
                return false;
            }
        }
        return true;
    }

    // Función para reiniciar el puzzle
    private void RestartPuzzle()
    {
        // Reiniciar las variables
        clickCount = 0;
        playerClicks = new GameObject[4]; // Crear un nuevo array para los clics

        // Cambiar el texto de feedback
        feedbackText.PopUpText("¡Uy! Ese no es.");

        // Volver a habilitar la interacción después de un pequeño retraso
        isPuzzleInProgress = false;
    }


    void MoveObject()
    {
        if (objectToMove != null)
        {
            objectToMove.position = newPosition;  // Cambia la posición
        }
    }
}
