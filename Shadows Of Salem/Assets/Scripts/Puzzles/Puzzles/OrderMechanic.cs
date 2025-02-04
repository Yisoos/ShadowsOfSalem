using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMechanic : MonoBehaviour
{
    [Tooltip("Arrastrar aquí los GameObjects dependiendo del orden correcto.")]
    public Transform[] correctOrder; 
    private Transform[] currentOrder; // Guarda el orden actual

    [Tooltip("Hacer referencia al script: OverlayDarkScreenControler.")]
    public OverlayDarkScreenControler darkOverlayControl; 
    private FeedbackTextController feedbackText;
    [Tooltip("Escribir aquí el mensaje que quieres que aparezca cuando se ha resuelto el puzle.")]
    public string feedbackMessage;

    private void Start()
    {
        feedbackText = FindObjectOfType<FeedbackTextController>();
    }
    public void UpdateOrder()
    {
        // Esta función registra el orden actual de los objetos secundarios dentro de un objeto padre y luego verifica si el orden es correcto según un orden predefinido
        Debug.Log("UpdateOrder called");

        // Obtener todos los objetos secundarios del padre en su orden aactual
        currentOrder = new Transform[correctOrder.Length];
        for (int i = 0; i < transform.childCount; i++) // recorremos cada hijo del objeto padre
        {
            currentOrder[i] = transform.GetChild(i); // almacenamos cada hijo en el indice correspondiente del array "currentOrder"
            Debug.Log($"Current Order[{i}] = {currentOrder[i].name}");
        }

        // llamar la funcion que comprueba si el orden es correcto
        CheckOrder();
    }

    private void CheckOrder()
    {
        bool isCorrect = true;

        // comprobar el orden
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }
        
        if (isCorrect)
        {
            Debug.Log("Correct Order!");
            if (darkOverlayControl != null)
            {
                darkOverlayControl.ConditionMet = true; // es correcto, desactivamos el overlay
            }
                feedbackText.PopUpText(feedbackMessage);
           
        }

        else
        {
            Debug.Log("Incorrect Order!"); // no es correcto, no hacemos nada
            if (darkOverlayControl != null)
            {
                darkOverlayControl.ConditionMet = false; // es correcto, desactivamos el overlay
            }
        }
    }

}
