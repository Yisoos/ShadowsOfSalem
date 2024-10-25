using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationLockPopUp : MonoBehaviour
{
    public CombinationLockControl combinationLock;
    public TMP_Text[] numbersInLock;

    public void CombinationLockLogic() //para candados de combinación
    {
        if (combinationLock.isLocked)
        {
            //popUpLockPrefab.SetActive(true);
            string digitsInLockAdded = null;
            for (int i = 0; i < numbersInLock.Length; i++)
            {
                digitsInLockAdded += numbersInLock[i].text;
            }
            Debug.Log(digitsInLockAdded);

            bool combinationMatch = digitsInLockAdded == combinationLock.combination;

            if (combinationMatch)
            {
                Debug.Log("Combinación correcta, Candado abierto");
                combinationLock.isLocked = false;
                if (combinationLock.feedbackText != null)
                {
                    Tags tag = combinationLock.gameObject.GetComponent<Tags>();
                    combinationLock.feedbackText.PopUpText(tag.objectDescription);
                    gameObject.SetActive(false);
                }
                TurnOnLockCollider();
            }
            else
            {
                Debug.Log("Contraseña incorrecta");
            }
        }
    }
    public void AddNumberToCombinationDigit(TMP_Text displayDigit)
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
    public void SubtractNumberToCombinationDigit(TMP_Text displayDigit)
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
    public void TurnOnLockCollider()
    {
        Collider2D objectCollider = combinationLock.GetComponent<Collider2D>();

        // Disable the Collider
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
            Debug.Log("Collider has been disabled.");
        }
    }
}
