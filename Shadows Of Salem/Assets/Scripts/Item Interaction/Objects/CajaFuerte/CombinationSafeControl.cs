using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinationSafeControl : MonoBehaviour
{
    public CombinationWheel combinationWheel;
    public GameObject safeDoor;
    public TMP_Text feedbacktext;
    public string correctCombination;
    public float feedbackDuration = 2f;


    public void OnWheelStopped()
    {
        //verifica si la combinacion es correcta
        if (combinationWheel.GetCombination() == correctCombination)
        {
            ShowFeedback("Caja fuerte abierta", true);
            safeDoor.SetActive(false);
        }

        else
        {
            ShowFeedback("Codigo incorrecto", false);
            combinationWheel.resetWheel();
        }
    }

    private void ShowFeedback(string message, bool v)
    {
        feedbacktext.text = message;
        Invoke("ClearFeedback", feedbackDuration);
    }

    private void ClearFeedback()
    {
        feedbacktext.text = "";
    }

}
