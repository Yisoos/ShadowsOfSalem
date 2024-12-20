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
    public bool ObjectAccessibilityChecker(Transform objectHit)
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Lock itemLocked = objectHit.GetComponent<Lock>();
        NonOrderedDependencies itemDependency = objectHit.GetComponent<NonOrderedDependencies>();
        OrderedDependencies itemDependencyByOrder = objectHit.GetComponent<OrderedDependencies>();
        CombinationLockControl combinationLocked = objectHit.GetComponent<CombinationLockControl>();
        SecretDoorLogic secretDoorLogic = objectHit.GetComponent<SecretDoorLogic>();

        // Si el objeto est� bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null && itemLocked.isLocked)
        {
            feedbackTextController.PopUpText(itemLocked.displayText[0]);
            return false;
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
        // Si el objeto est� en un candado de combinaci�n, muestra mensaje y devuelve falso
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
    public void TryAccess(InventoryItem objectDropped, Transform objectHit)
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Lock itemLocked = objectHit.GetComponent<Lock>();
        NonOrderedDependencies itemDependencies = objectHit.GetComponent<NonOrderedDependencies>();
        OrderedDependencies itemDependencyByOrder = objectHit.GetComponent<OrderedDependencies>();
        ObjectCombination itemObjectCombination = objectHit.GetComponent<ObjectCombination>();
        InterchangableItemPlacement itemInterchangableItemPlacement = objectHit.GetComponent<InterchangableItemPlacement>();
        // Si el objeto est� bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null)
        {
            Key key = objectDropped.GetComponent<Key>();
            if (key != null)
            {
            itemLocked.TryUnlock(key);
            }
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
    public bool isUIObjectInteractable(InventoryItem ObjectDropped, InventoryItem ObjectInInventorySlot) 
    {
        //Debug.Log($"{ObjectDropped.objectName} dropped onto {ObjectInInventorySlot.objectName}");
        ObjectCombinationInInventory itemCombination = ObjectDropped.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(ObjectInInventorySlot.itemTag.objectName)) 
        {
            return itemCombination.CheckForCombination(ObjectDropped, ObjectInInventorySlot);
        }
        itemCombination = ObjectInInventorySlot.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(ObjectDropped.itemTag.objectName))
        {
                return itemCombination.CheckForCombination(ObjectInInventorySlot, ObjectDropped);
        }
        return false;
    }
}
