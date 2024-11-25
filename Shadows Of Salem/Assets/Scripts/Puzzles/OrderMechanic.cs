using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMechanic : MonoBehaviour
{
    public Transform[] correctOrder; // Assign the correct order of objects in the Inspector
    private Transform[] currentOrder; // Store the current order of objects

    public GameObject pantallaNegro;
    public FeedbackTextController feedbackText;

    public void Start()
    {
        pantallaNegro.SetActive(true);
    }
    public void UpdateOrder()
    {
        // Log to confirm the function is called
        Debug.Log("UpdateOrder called");

        // Get all child objects of the parent in their current hierarchy order
        currentOrder = new Transform[correctOrder.Length];
        for (int i = 0; i < transform.childCount; i++)
        {
            currentOrder[i] = transform.GetChild(i);
            Debug.Log($"Current Order[{i}] = {currentOrder[i].name}");
        }

        // Check if the current order matches the correct order
        CheckOrder();
    }

    private void CheckOrder()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }

        // Provide feedback
        if (isCorrect)
        {
            Debug.Log("Correct Order!");
            pantallaNegro.SetActive(false);
            feedbackText.PopUpText("¡Ese sonido otra vez! ¿He activado algo?");
        }

        else
        {
            Debug.Log("Incorrect Order!");
        }
    }
}
