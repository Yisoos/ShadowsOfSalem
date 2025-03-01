using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroFilmController : MonoBehaviour
{
    LightSwitch lightswitch;
    public Transform microfilmLightOff;
    public string lightOffMessage;
    public Transform microfilmLightOn;
    public string lightOnMessage;
    public bool isPopUpOpen;
    public LightSwitch lightSwitch;

    private FeedbackTextController feedbackText;

    private void Start()
    {
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        TurnOffPopUp();
    }
    private void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform) && !isPopUpOpen)
        {
            TurnOnPopUp();
        }
    }

    public void TurnOnPopUp() 
    {
        if (lightSwitch.IsAccessible())
        {
            microfilmLightOff.gameObject.SetActive(true);
            feedbackText.PopUpText(lightOffMessage);
        }
        else
        {
            microfilmLightOn.gameObject.SetActive(true);
            feedbackText.PopUpText(lightOnMessage);

        }
        gameObject.SetActive(false);
        isPopUpOpen = true;
    }
    public void TurnOffPopUp()
    {
        gameObject.SetActive(true);
        microfilmLightOff.gameObject.SetActive(false);
        microfilmLightOn.gameObject.SetActive(false);
        isPopUpOpen = false;
    }
}
