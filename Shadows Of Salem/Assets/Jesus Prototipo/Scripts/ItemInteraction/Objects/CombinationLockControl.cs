using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationLockControl : MonoBehaviour
{
    public bool isLocked; // Indica si el candado está cerrado
    public FeedbackTextController feedbackText;
    public string combination; // La clave que desbloquea este candado
    public GameObject popUpLockPrefab;
    public Transform PopUpLockParent;
   
    public void OnMouseDown()
    {
        if (isLocked)
        {
            PopUpWindowManager();
        }
    }
    public void PopUpWindowManager()
    {
        if (isLocked && popUpLockPrefab!=null) 
        {
            Tags prefabPopUpTags = popUpLockPrefab.GetComponent<Tags>();
            Tags[] allTagsInScene = FindObjectsOfType<Tags>();
            foreach (Tags tag in allTagsInScene) 
            {
                if (tag.objectName == prefabPopUpTags.objectName) 
                {
                    tag.gameObject.SetActive(true);
                }
                else 
                {
                    GameObject popUp = Instantiate(popUpLockPrefab,PopUpLockParent);
                    popUp.transform.SetAsLastSibling();
                    CombinationLockPopUp PopUpScript= popUp.GetComponent<CombinationLockPopUp>();
                    PopUpScript.combinationLock = this;
                    break;
                }
            }
            Collider2D objectCollider = GetComponent<Collider2D>();
            // Disable the Collider
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
                Debug.Log("Collider has been disabled.");
            }
        }
    }
}
