using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTextTrigger : MonoBehaviour
{
    [Space(5)] public string[] displayText;
    [Space(5)] public bool randomize;

    private FeedbackTextController feedbackText;
    private int index;
    private int oldIndex;
    private void Start()
    {
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        index = 0;
        oldIndex = index;
    }
    private void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.IsAccessibleOnMousedown(this.transform))
        {
            DisplayAllText();
        }
    }
    public void DisplayAllText()
    {
        if (randomize)
        {
            do
            {
                index = Random.Range(0, displayText.Length - 1);
            } 
            while (displayText.Length > 1 && index == oldIndex);

            feedbackText.PopUpText(displayText[index]);
            oldIndex = index;
        }
        else
        {
            feedbackText.PopUpText(displayText[index]);
            index = index >= displayText.Length-1 ? 0 : index + 1;
        }
    }
}
