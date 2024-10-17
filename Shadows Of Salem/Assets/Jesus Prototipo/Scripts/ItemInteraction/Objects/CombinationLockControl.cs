using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationLockControl : MonoBehaviour
{
    public bool isLocked; // Indica si el candado está cerrado
    public FeedbackTextController feedbackText;
    public string combination; // La clave que desbloquea este candado
    public TMP_Text[] numbersInLock;
    public GameObject popUpCombinationLock;
    
    public void CombinationLockLogic() //para candados de combinación
    {
        if ( isLocked)
        {
            //popUpCombinationLock.SetActive(true);
            string digitsInLockAdded = null;
            for (int i = 0; i < numbersInLock.Length; i++)
            {
                digitsInLockAdded += numbersInLock[i].text;
            }
            Debug.Log(digitsInLockAdded);
            
            bool combinationMatch = digitsInLockAdded==combination;
                
            if (combinationMatch)
            {
                Debug.Log("Combinación correcta, Candado abierto");
                isLocked = false;
                if (feedbackText != null)
                {
                    Tags tag = gameObject.GetComponent<Tags>();
                    feedbackText.PopUpText(tag.objectDescription);
                }
            }
            else
            {
                Debug.Log("Contraseña incorrecta");
            }
        }
    }
    public void AddNumberToCombinationDigit(TMP_Text displayDigit)
    {
        if (isLocked)
        { 
            int digit = Convert.ToInt32(displayDigit.text);
            digit = digit + 1 > 9 ? 0 : digit + 1;
            displayDigit.text = Convert.ToString(digit);
            for (int i = 0; i < numbersInLock.Length; i++)
            {
                if (numbersInLock[i] == displayDigit)
                {
                    numbersInLock[i].text = displayDigit.text;
                }
            }
            Debug.Log($"Valor en el candado: {numbersInLock[0].text}{numbersInLock[1].text}{numbersInLock[2].text}{numbersInLock[3].text}");
            CombinationLockLogic();
        }
    }
    public void SubtractNumberToCombinationDigit(TMP_Text displayDigit)
    {
        if (isLocked)
        {
            int digit = Convert.ToInt32(displayDigit.text);
            digit = digit - 1 < 0 ? 9 : digit - 1;
            displayDigit.text = Convert.ToString(digit);
            for (int i = 0; i < numbersInLock.Length; i++)
            {
                if (numbersInLock[i] == displayDigit)
                {
                    numbersInLock[i].text = displayDigit.text;
                }
            }
            Debug.Log($"Valor en el candado: {numbersInLock[0].text}{numbersInLock[1].text}{numbersInLock[2].text}{numbersInLock[3].text}");
            CombinationLockLogic();
        }
    }
}
