using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotaryDialControl : MonoBehaviour
{
    [Header("Ajustes del Telefono")]
    [Tooltip("Separa cada número con un '-' ")]public string numberToCall; // The key to unlock this lock
    [Space(5)]public GameObject dialDisplayPrefab;
    [Space(5), Range(0, 15)] public float dialReturnSpeed; // Velocidad de retorno del dial a su posición inicial

    [Space(10), Header("Ajustes de Texto")]
    public string[] displayText;
    [Space(5)] public TMP_FontAsset[] fontAsset;
    
    [Space(10), Header("Otros Ajustes")]
    public Transform UIInventoryDisplay;

    [HideInInspector] public FeedbackTextController feedbackText;
    [HideInInspector] public Transform dialDisplayParent;

    private void Start()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        dialDisplayParent = FindObjectOfType<Canvas>().transform;
    }

    public void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.transform)) 
        { 
            StartCoroutine(PopUpWindowManager());
        }
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f); // Delay for half a second (adjust as necessary)

        if (dialDisplayPrefab != null)
        {
            RotaryDial[] allDialsInScene = GameObject.FindObjectsOfType<RotaryDial>(true);
            bool foundMatchingTag = false; // Track if a matching tag is found

            foreach (RotaryDial dial in allDialsInScene)
            {
                if (dial.phoneParent == this)
                {
                    dial.gameObject.SetActive(true);
                    dial.transform.SetAsLastSibling();
                    foundMatchingTag = true; // Mark that we found a match
                    break; // Exit loop once a match is found
                }
            }

            // If no matching tag was found, instantiate a new pop-up
            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(dialDisplayPrefab, dialDisplayParent);
                RotaryDial popUpScript = popUp.GetComponentInChildren<RotaryDial>();
                popUpScript.phoneParent = this;
                popUpScript.phoneNumberDisplay.text = string.Empty;
                popUp.transform.SetAsLastSibling();
            }

            // Disable the Collider
            Collider2D objectCollider = GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
                //Debug.Log("Collider has been disabled.");
            }
        }
    }
}
