using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotaryDialControl : MonoBehaviour
{
    [Space(10)] public FeedbackTextController feedbackText;
    [Space(10),Tooltip("Separa cada número con un '-' ")]public string PhoneNumberToDial; // The key to unlock this lock
    [Space(10)]public GameObject dialDisplayPrefab;
    [Space(10)] public Transform dialDispalyParent;

    public void OnMouseDown()
    {
        
            StartCoroutine(PopUpWindowManager());
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f); // Delay for half a second (adjust as necessary)

        if (dialDisplayPrefab != null)
        {
            Tags prefabPopUpTags = dialDisplayPrefab.GetComponent<Tags>();
            Tags[] allTagsInScene = GameObject.FindObjectsOfType<Tags>(true);
            bool foundMatchingTag = false; // Track if a matching tag is found

            foreach (Tags tag in allTagsInScene)
            {
                if (tag.objectName == prefabPopUpTags.objectName)
                {
                    tag.gameObject.SetActive(true);
                    tag.transform.SetAsLastSibling();
                    foundMatchingTag = true; // Mark that we found a match
                    break; // Exit loop once a match is found
                }
            }

            // If no matching tag was found, instantiate a new pop-up
            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(dialDisplayPrefab, dialDispalyParent);
                popUp.transform.SetAsLastSibling();
                RotaryDial popUpScript = popUp.GetComponent<RotaryDial>();
                popUpScript.phoneParent = this;
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
    [ContextMenu("Conectar componentes generales")]
    private void ConectarComponentesGenerales()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        dialDispalyParent = FindObjectOfType<Canvas>().transform;
    }
}
