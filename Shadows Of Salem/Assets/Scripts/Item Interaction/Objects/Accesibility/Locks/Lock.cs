using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

// Clase que representa un candado
public class Lock : MonoBehaviour
{   
    [Space(5)] public Transform lockedObject;
    [Space(5)] public int lockID; // El ID de este candado
    [Space(5)] public Sprite unlockedSprite;
    [Space(5), Tooltip("Elemento 0: Mensaje de necesito llave/esta cerrado\n" +
                       "Elemento 1: Objeto no es una llave\n" +
                       "Elemento 2: Llave equivocada\n" +
                       "Elemento 3: Mensaje de puerta Abierta")] 
    public string[] displayText = new string[4];

    [HideInInspector] public bool isLocked = true; // Indica si el candado está cerrado
    private Inventory inventory; // Referencia al inventario asociado
    private FeedbackTextController feedbackText;
    private LockedObject lockedObjectControl;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = FindAnyObjectByType<Inventory>(); // Referencia al inventario asociado
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        if (unlockedSprite == null && spriteRenderer != null)
        {
            unlockedSprite = spriteRenderer.sprite;
        }
        if (lockedObject == null)
        {
            lockedObject = this.transform;
        }
        lockedObjectControl = lockedObject.GetComponent<LockedObject>();
        if (lockedObject != this.transform && lockedObjectControl == null)
        {
            lockedObjectControl = lockedObject.AddComponent<LockedObject>();
        }
        if(lockedObjectControl != null)
        {
            lockedObjectControl.parentLock = this;
        }
    }
    public void OnMouseDown()
    {
        AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.transform);
    }
    public bool CheckIfLocked()
    {
        FeedbackTextTrigger feedbackTextTrigger = GetComponent<FeedbackTextTrigger>();
        if (isLocked)
        {
            if (feedbackTextTrigger != null)
            {
                feedbackTextTrigger.DisplayAllText();
            }
            else
            {
                feedbackText.PopUpText(displayText[0]);
            }
            return false;
        }
        return true;
    }
    // Método que intenta desbloquear el candado con una llave
    public void TryUnlock(InventoryItem itemDropped) //para candados con llave
    {
        Key key = itemDropped.GetComponentInParent<Key>();
        // Verifica si la llave no es nula, si su ID coincide con el del candado y si está bloqueado
        if (key != null)
        {
            if (key.keyID == lockID)
            {
                if (isLocked) 
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    isLocked = false; 
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = unlockedSprite;
                    }
                    if (feedbackText != null) 
                    { 
                        feedbackText.PopUpText(displayText[3]);
                    }
                    inventory.DeleteItem(itemDropped,1);
                    if (lockedObject != this.transform) 
                    {
                        lockedObjectControl.OpenUp();
                    }
                }
            }
            else
            {
                feedbackText.PopUpText(displayText[2]);
            }
        }
        else
        {
            feedbackText.PopUpText(displayText[1]);
        }
    }
}
