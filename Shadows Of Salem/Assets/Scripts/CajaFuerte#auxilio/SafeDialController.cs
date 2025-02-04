using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDialController : MonoBehaviour
{
    public int currentNumber = 0; // Número actual en la rueda (0-19)
    public float holdTime = 2f; // Tiempo que el usuario debe mantener el número en la rueda
    private float timer = 0f; // Temporizador para contar los 2 segundos
    public bool isHolding = false; // Indica si el usuario está manteniendo un número
    public SafeCombinationController combinationController; // Referencia al script de la contraseña



    void Update()
    {
        if (isHolding) // Si el usuario esta marcando un numero 
        {
            timer += Time.deltaTime;

            if (timer >= holdTime) // Si llega a dos segundos comprueba el numero
            {
                combinationController.CheckNumber(currentNumber); // Verifica si es el numero correcto
                isHolding = false; // Reinicia el estado
                timer = 0f; 
            }
        }
    }


    public void RotateLeft()// Método para girar la rueda a la izquierda 
    {

        currentNumber = (currentNumber - 1 + 20) % 20; // Va hacia atras disminuyendo el numero (0, 20, 19..)
        Debug.Log("left");
        transform.Rotate(0, 0, -18); // Gira la rueda 18 grados a la izquierda
        isHolding = true; // Comprueba que el usuario está manteniendo un número

    }

    public void RotateRight()// Método para girar la rueda a la derecha
    {

        currentNumber = (currentNumber + 1) % 20; // Va hacia delante aumentando el numero (0, 1, 2..)
        Debug.Log("Right");
        transform.Rotate(0, 0, 18); 
        isHolding = true; 

    }

}
