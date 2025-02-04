using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public SafeDialController safeDial; // referencia al script
    public bool isLeft; // la flecha izquierda

    private void OnMouseDown()
    {
        if (isLeft)
        {
            safeDial.RotateLeft();
        }

        else
        {
            safeDial.RotateRight();
        }
    }
}
