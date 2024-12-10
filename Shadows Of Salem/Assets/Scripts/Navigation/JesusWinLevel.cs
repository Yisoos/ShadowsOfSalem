using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JesusWinLevel : MonoBehaviour
{
    public string sceneToGoToWhenFinished;
    public string[] RequiredItems;
    public string failMessage;
    public void OnMouseDown() 
    {
        PassLevel();
    }
    public bool IsReadyToFinish() 
    { 
        NewInventory inventory = FindAnyObjectByType<NewInventory>();
        FeedbackTextController feedbackTextController = FindAnyObjectByType<FeedbackTextController>();
        for (int i = 0;i < RequiredItems.Length; i++) 
        {
            if (!inventory.items.Find(currentItem => currentItem.tagInfo.objectName == RequiredItems[i])) 
            {
                feedbackTextController.PopUpText(failMessage);
                return false;
            }
        }
        return true; 
    }

    public void PassLevel() 
    {
        if (IsReadyToFinish())
        {
            CambiarEscenas scenesManager = FindAnyObjectByType<CambiarEscenas>();
            scenesManager.ChangeToScene(sceneToGoToWhenFinished);
        }
    } 
}
