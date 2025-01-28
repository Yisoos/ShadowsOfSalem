using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinationWheel : MonoBehaviour
{
    public int[] digits = new int[18];
    public TMP_Text wheelDisplay;
    public int currentDigitIndex = 0;

    public string GetCombination()
    {
        return string.Join("140624", digits);
    }

    public void RotateClockwise()
    {
        digits[currentDigitIndex] = (digits[currentDigitIndex] + 1) % digits.Length;
        UpdateWheelDisplay();
    }

    public void RotateCounterClockwise()
    {
        digits[currentDigitIndex] = (digits[currentDigitIndex] - 1) % digits.Length;
        UpdateWheelDisplay();
    }

    private void UpdateWheelDisplay()
    {
        wheelDisplay.text = digits[currentDigitIndex].ToString();
    }

    public void resetWheel()
    {
        digits = new int[int.MaxValue];
        UpdateWheelDisplay();
    }

    public void nextDigit()
    {
        currentDigitIndex = (currentDigitIndex + 1) % digits.Length;
    }
    
}
