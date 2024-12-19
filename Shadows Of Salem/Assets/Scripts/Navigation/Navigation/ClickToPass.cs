using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPass : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        {
            WinLevel WinLevel = FindAnyObjectByType<WinLevel>();
            WinLevel.PassLevel();
        }
    }
}
