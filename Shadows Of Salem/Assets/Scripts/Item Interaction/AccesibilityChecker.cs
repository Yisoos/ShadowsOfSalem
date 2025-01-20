using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesibilityChecker : MonoBehaviour
{
    public static AccesibilityChecker Instance;
    public FeedbackTextController feedbackTextController;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public bool ObjectAccessibilityChecker(Transform objectClicked)
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Lock itemLocked = objectClicked.GetComponent<Lock>();
        LockedObject itemLockedObject = objectClicked.GetComponent<LockedObject>();
        NonOrderedDependencies itemDependency = objectClicked.GetComponent<NonOrderedDependencies>();
        OrderedDependencies itemDependencyByOrder = objectClicked.GetComponent<OrderedDependencies>();
        CombinationLockControl combinationLocked = objectClicked.GetComponent<CombinationLockControl>();
        SecretDoorLogic secretDoorLogic = objectClicked.GetComponent<SecretDoorLogic>();

        // Si el objeto está bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null && itemLocked.isLocked)
        { 
            return itemLocked.CheckIfLocked();
        }
        if (itemLockedObject != null && itemLockedObject.parentLock.isLocked)
        {
            return itemLockedObject.CheckIfParentIsLocked();
        }
        // Si el objeto tiene dependencias no cumplidas, muestra mensaje y devuelve falso
        if (itemDependency != null)
        {
            return itemDependencyByOrder.FeedbackTextDesider();
        }
        if (itemDependencyByOrder != null)
        {
            return itemDependencyByOrder.FeedbackTextDesider();
        }
        // Si el objeto está en un candado de combinación, muestra mensaje y devuelve falso
        if (combinationLocked != null && combinationLocked.isLocked)
        { 
            return false;
        }
        if (secretDoorLogic != null)
        {
            return !secretDoorLogic.isSolved();
        }
        // Si no hay restricciones, devuelve verdadero
        return true;
    }
    public void TryAccess(InventoryItem objectDropped, Transform objectClicked)
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Lock itemLocked = objectClicked.GetComponent<Lock>();
        LockedObject itemLockedObject = objectClicked.GetComponent<LockedObject>();
        NonOrderedDependencies itemDependencies = objectClicked.GetComponent<NonOrderedDependencies>();
        OrderedDependencies itemDependencyByOrder = objectClicked.GetComponent<OrderedDependencies>();
        ObjectCombination itemObjectCombination = objectClicked.GetComponent<ObjectCombination>();
        InterchangableItemPlacement itemInterchangableItemPlacement = objectClicked.GetComponent<InterchangableItemPlacement>();
        // Si el objeto está bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null)
        {
            itemLocked.TryUnlock(objectDropped);
            return;
        }
        if (itemLockedObject != null)
        {
            itemLockedObject.CheckForKey(objectDropped);
            return;
        }
        // Si el objeto tiene dependencias no cumplidas, muestra mensaje y devuelve falso
        if (itemDependencies != null)
        {
            itemDependencies.HandleItem(objectDropped);
            return;
        }
        if (itemDependencyByOrder != null)
        {
            itemDependencyByOrder.HandleItem(objectDropped);
        }
        if (itemObjectCombination != null)
        {
            itemObjectCombination.CheckForCombination(objectDropped);
        }
        if (itemInterchangableItemPlacement != null)
        {
            itemInterchangableItemPlacement.PlaceOrSwapItem(objectDropped);
        }
    }
    public bool IsAccessibleOnMousedown(Transform objectClicked)
    {
        Lock itemLocked = objectClicked.GetComponent<Lock>();
        LockedObject itemLockedObject = objectClicked.GetComponent<LockedObject>();
        OrderedDependencies itemDependencyByOrder = objectClicked.GetComponent<OrderedDependencies>();
        FeedbackTextTrigger feedbackTextTrigger = objectClicked.GetComponent<FeedbackTextTrigger>();
        if (feedbackTextTrigger != null)
        {
            if (itemLocked != null || itemDependencyByOrder != null || itemLockedObject != null)
            {
                return false;
            }
        }
        return true;
    }
    public bool IsUIObjectInteractable(InventoryItem objectDropped, InventoryItem objectInInventorySlot) 
    {
        //Debug.Log($"{objectDropped.objectName} dropped onto {objectInInventorySlot.objectName}");
        ObjectCombinationInInventory itemCombination = objectDropped.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(objectInInventorySlot.itemTag.objectName)) 
        {
            return itemCombination.CheckForCombination(objectDropped, objectInInventorySlot);
        }
        itemCombination = objectInInventorySlot.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(objectDropped.itemTag.objectName))
        {
                return itemCombination.CheckForCombination(objectInInventorySlot, objectDropped);
        }
        return false;
    }
}
