using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDialController : MonoBehaviour
{
    public int currentNumber = 0; // Número actual en la rueda (0-19)
    public float holdTime = 2f; // Tiempo que el usuario debe mantener el número en la rueda
    public SafeCombinationController combinationController; // Referencia al script de la contraseña

    public IEnumerator HoldTime() 
    {
        yield return new WaitForSeconds(holdTime);
        combinationController.CheckNumber(currentNumber); // Verifica si es el numero correcto
    }

    public void RotateLeft()// Método para girar la rueda a la izquierda 
    {
        StopAllCoroutines();
        StartCoroutine(HoldTime());
        currentNumber = (currentNumber - 1 + 20) % 20; // Va hacia atras disminuyendo el numero (0, 20, 19..)
        //Debug.Log("left");
        transform.Rotate(0, 0, -18); // Gira la rueda 18 grados a la izquierda
    }

    public void RotateRight()// Método para girar la rueda a la derecha
    {
        StopAllCoroutines();
        StartCoroutine(HoldTime());
        currentNumber = (currentNumber + 1) % 20; // Va hacia delante aumentando el numero (0, 1, 2..)
        //Debug.Log("Right");
        transform.Rotate(0, 0, 18);
    }

}
