using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPass : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        {
            JesusWinLevel jesusWinLevel = FindAnyObjectByType<JesusWinLevel>();
            jesusWinLevel.PassLevel();
        }
    }
}
