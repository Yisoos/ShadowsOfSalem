using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroFilmController : MonoBehaviour
{
    LightSwitch lightswitch;
    public Collider2D ZoomedOutMicrofilm;
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
    private void OnDisable()
    {
        TurnOffPopUp();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checks for left mouse button click
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == ZoomedOutMicrofilm)
            {
                if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform) && !isPopUpOpen)
                {
                    TurnOnPopUp();
                }
            }
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
        ZoomedOutMicrofilm.gameObject.SetActive(false);
        isPopUpOpen = true;
    }
    public void TurnOffPopUp()
    {
        ZoomedOutMicrofilm.gameObject.SetActive(true);
        microfilmLightOff.gameObject.SetActive(false);
        microfilmLightOn.gameObject.SetActive(false);
        isPopUpOpen = false;
    }
}
