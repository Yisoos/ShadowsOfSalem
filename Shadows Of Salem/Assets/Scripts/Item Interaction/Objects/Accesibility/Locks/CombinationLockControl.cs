using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationLockControl : MonoBehaviour
{
    public bool isLocked; // Indicates if the lock is closed
    public string combination; // The key to unlock this lock
    public GameObject popUpLockPrefab;
    public string[] displayText;

    [HideInInspector] public Transform popUpLockParent;
    [HideInInspector] public FeedbackTextController feedbackText;

    private void Start()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        popUpLockParent = FindObjectOfType<Canvas>().transform;
    }

    public void OnMouseDown()
    {
        if (isLocked)
        {
            StartCoroutine(PopUpWindowManager());
        }
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f); // Delay for half a second (adjust as necessary)

        if (isLocked && popUpLockPrefab != null)
        {

            CombinationLockPopUp[] allComLocksInScene = GameObject.FindObjectsOfType<CombinationLockPopUp>(true);
            bool foundMatchingTag = false; // Track if a matching tag is found

            foreach (CombinationLockPopUp combLock in allComLocksInScene)
            {
                if (combLock.combinationLock == this)
                {
                    combLock.gameObject.SetActive(true);
                    foundMatchingTag = true; // Mark that we found a match
                    break; // Exit loop once a match is found
                }
            }

            // If no matching tag was found, instantiate a new pop-up
            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(popUpLockPrefab, popUpLockParent);
                popUp.transform.SetAsLastSibling();
                CombinationLockPopUp popUpScript = popUp.GetComponent<CombinationLockPopUp>();
                popUpScript.combinationLock = this;
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
