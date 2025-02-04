using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCombinationController : MonoBehaviour
{

    public int[] correctCombination = { 14, 6, 11 }; // Contraseña correcta (14-06-11)
    private int currentIndex = 0; // Estado inicial de la contraseña
    public FeedbackTextController feedbackText; // Referencia al FeedbackTextController
    public GameObject closeUpObject; // Referencia al objeto de close up


    // verifica si el número introducido es correcto
    public void CheckNumber(int number)
    {

        // Si el número es correcto
        if (number == correctCombination[currentIndex])
        {
            currentIndex++; // Avanza al siguiente número de la contraseña

            feedbackText.PopUpText("Código: " + GetCurrentCode()); // Muestra el código introducido


            // Si se ha introducido toda la contraseña, desbloquea la caja fuerte
            if (currentIndex >= correctCombination.Length)
            {
                UnlockSafe();
            }
        }

        else // Si el número es incorrecto, reinicia la secuencia
        {
            
            currentIndex = 0;
            feedbackText.PopUpText("Código incorrecto. Inténtalo de nuevo.");
        }

    }



    // Método para obtener el código introducido correctamente hasta el momento
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


