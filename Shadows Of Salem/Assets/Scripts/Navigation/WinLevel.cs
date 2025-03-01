using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    public Transform[] requiredObjectAccess;

    public string sceneToGoToWhenFinished;
    public string[] RequiredItems;
    public string failMessage;

    private Inventory inventory;
    private FeedbackTextController feedbackTextController;
    private CambiarEscenas scenesManager;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
        feedbackTextController = FindAnyObjectByType<FeedbackTextController>();
        scenesManager = FindAnyObjectByType<CambiarEscenas>();
    }

    public void TryToWinLevel()
    {
        foreach (Transform requiredObject in requiredObjectAccess)
        {
            if(!AccesibilityChecker.Instance.ObjectAccessibilityChecker(requiredObject))
            return;
        }
        PassLevel();
    }

    public void OnMouseDown()
    {
        TryToWinLevel(); // Ensures conditions are checked
    }

    public bool IsReadyToFinish()
    {
        if (inventory == null || inventory.items == null)
        {
            Debug.LogWarning("Inventory not found or empty!");
            return false;
        }

        foreach (var requiredItem in RequiredItems)
        {
            if (!inventory.items.Exists(currentItem => currentItem.itemTag.objectName == requiredItem))
            {
                feedbackTextController?.PopUpText(failMessage);
                return false;
            }
        }
        return true;
    }

    public void PassLevel()
    {
        if (IsReadyToFinish() && AccesibilityChecker.Instance.ObjectAccessibilityChecker(transform))
        {
            scenesManager?.ChangeToScene(sceneToGoToWhenFinished);
        }
    }
}
