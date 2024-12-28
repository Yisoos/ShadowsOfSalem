using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : MonoBehaviour
{
    [Space(5)] public string textWhenLocked;
    [Space(5)] public string rightKeyText;
    [Space(5)] public Sprite unlockedSprite;
    
    [HideInInspector]public Lock parentLock;
    private FeedbackTextController feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        if (string.IsNullOrEmpty(textWhenLocked))
            textWhenLocked = "Está cerrado...";
        if (string.IsNullOrEmpty(rightKeyText))
            textWhenLocked = "Esa es la llave, tengo que ponerla en el lugar correcto";
        if (unlockedSprite == null)
        {
            unlockedSprite = spriteRenderer.sprite;
        }
    }
    public void OnMouseDown()
    {
        CheckIfParentIsLocked();
    }
    public bool CheckIfParentIsLocked() 
    {
        FeedbackTextTrigger feedbackTextTrigger = GetComponent<FeedbackTextTrigger>();
        if (parentLock.isLocked)
        {
            if (feedbackTextTrigger != null)
            {
                feedbackTextTrigger.DisplayAllText();
            }
            else
            {
                feedbackText.PopUpText(textWhenLocked);
            }
            return false;
        }
        return true;
    }
    public void CheckForKey(InventoryItem itemDropped)
    {
        Key key = itemDropped.GetComponentInParent<Key>();
        // Verifica si la llave no es nula, si su ID coincide con el del candado y si está bloqueado
        if (key != null)
        {
            if (key.keyID == parentLock.lockID)
            {
                if (parentLock.isLocked)
                {
                    feedbackText.PopUpText(rightKeyText);
                }
            }
            else
            {
                feedbackText.PopUpText(parentLock.displayText[2]);
            }
        }
        else
        {
            feedbackText.PopUpText(parentLock.displayText[1]);
        }
    }
    public void OpenUp() 
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer!=null && unlockedSprite != null) 
        {
            spriteRenderer.sprite = unlockedSprite;
        }
    }
}
