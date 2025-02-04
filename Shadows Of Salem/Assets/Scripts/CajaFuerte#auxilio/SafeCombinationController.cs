using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCombinationController : MonoBehaviour
{

    public int[] correctCombination = { 14, 6, 11 }; // Contrase�a correcta (14-06-11)
    private int currentIndex = 0; // Estado inicial de la contrase�a
    public FeedbackTextController feedbackText; // Referencia al FeedbackTextController
    public GameObject closeUpObject; // Referencia al objeto de close up


    // verifica si el n�mero introducido es correcto
    public void CheckNumber(int number)
    {

        // Si el n�mero es correcto
        if (number == correctCombination[currentIndex])
        {
            currentIndex++; // Avanza al siguiente n�mero de la contrase�a

            feedbackText.PopUpText("C�digo: " + GetCurrentCode()); // Muestra el c�digo introducido


            // Si se ha introducido toda la contrase�a, desbloquea la caja fuerte
            if (currentIndex >= correctCombination.Length)
            {
                UnlockSafe();
            }
        }

        else // Si el n�mero es incorrecto, reinicia la secuencia
        {
            
            currentIndex = 0;
            feedbackText.PopUpText("C�digo incorrecto. Int�ntalo de nuevo.");
        }

    }



    // M�todo para obtener el c�digo introducido correctamente hasta el momento
    private string GetCurrentCode()
    {
        string code = "";

        for (int i = 0; i < currentIndex; i++)
        {
            code += correctCombination[i].ToString("00") + " "; 
        }
        return code.Trim(); // Elimina el espacio final
    }



    // desbloquear la caja fuerte
    private void UnlockSafe()
    {
        feedbackText.PopUpText("Caja fuerte desbloqueada.");

        // Cerrar el close up
        if (closeUpObject != null)
        {
            closeUpObject.SetActive(false); 
        }
     }

}


