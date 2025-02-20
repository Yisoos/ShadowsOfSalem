using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public string sceneToGoToWhenFinished;
    public string[] RequiredItems;
    public string failMessage;
    public Transform 
    public void OnMouseDown() 
    {
        PassLevel();
    }
    public bool IsReadyToFinish() 
    { 
        Inventory inventory = FindAnyObjectByType<Inventory>();
        FeedbackTextController feedbackTextController = FindAnyObjectByType<FeedbackTextController>();
        for (int i = 0;i < RequiredItems.Length; i++) 
        {
            if (!inventory.items.Find(currentItem => currentItem.itemTag.objectName == RequiredItems[i])) 
            {
                feedbackTextController.PopUpText(failMessage);
                return false;
            }
        }
        return true; 
    }

    public void PassLevel() 
    {
        if (IsReadyToFinish() && AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        {
            CambiarEscenas scenesManager = FindAnyObjectByType<CambiarEscenas>();
            scenesManager.ChangeToScene(sceneToGoToWhenFinished);
        }
    } 
}
