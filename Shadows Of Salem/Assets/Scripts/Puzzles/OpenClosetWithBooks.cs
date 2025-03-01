using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenClosetWithBooks : MonoBehaviour
{
    public OrderMechanic[] orders;
    public Lock lockedObject;
    public string unlockedMessage;
    public string lockingMessage;
    [HideInInspector] public bool unLocked;
    private bool newUnlocked;
    FeedbackTextController feedbackText;
    private void Start()
    {
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
    }
    private void Update()
    {
        CheckCorrectOrders();
        if (unLocked) 
        {
            lockedObject.Unlock();   
        }
        else
        {
            lockedObject.isLocked = true;
        }
    }
    public void CheckCorrectOrders()
    {
        unLocked = orders.All(order => order.isCorrect);

        if (unLocked != newUnlocked)  // Detect change
        {
            newUnlocked = unLocked;
            Debug.Log("Unlock state changed: " + unLocked);
            feedbackText.PopUpText(unLocked ? unlockedMessage:lockingMessage);
        }
    }
}
