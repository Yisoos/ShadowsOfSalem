using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class JesusWinLevel : MonoBehaviour
{
    public string sceneToGoToWhenFinished;
    public string[] RequiredItems;
    public string failMessage;
    public void OnMouseDown() 
    {
        if (IsReadyToFinish())
        {
            CambiarEscenas scenesManager = FindAnyObjectByType<CambiarEscenas>();
            scenesManager.ChangeToScene(sceneToGoToWhenFinished);
        }
    }
    public bool IsReadyToFinish() 
    { 
        Inventory inventory = FindAnyObjectByType<Inventory>();
        FeedbackTextController feedbackTextController = FindAnyObjectByType<FeedbackTextController>();
        for (int i = 0;i < RequiredItems.Length; i++) 
        {
            if (!inventory.items.Find(currentItem => currentItem.objectName == RequiredItems[i])) 
            {
                feedbackTextController.PopUpText(failMessage);
                return false;
            }
        }
        return true; 
    }
}
