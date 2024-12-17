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
        DependencyHandler itemDependencies = objectHit.GetComponent<DependencyHandler>();
        OrderedDependencies itemDependencyByOrder = objectHit.GetComponent<OrderedDependencies>();
        CombinationLockControl combinationLocked = objectHit.GetComponent<CombinationLockControl>();
        SecretDoorLogic secretDoorLogic = objectHit.GetComponent<SecretDoorLogic>();

        // Si el objeto está bloqueado, muestra mensaje y devuelve falso
        if (itemLocked != null && itemLocked.isLocked)
        {
            feedbackTextController.PopUpText(itemLocked.displayText[0]);
            return false;
        }
        // Si el objeto tiene dependencias no cumplidas, muestra mensaje y devuelve falso
        if (itemDependencies != null && !itemDependencies.dependencyMet)
        {
            feedbackTextController.PopUpText(itemDependencies.displayText[0]);
            return false;
        }
        if (itemDependencyByOrder != null)
        {

            return itemDependencyByOrder.feedbackTextDesider();
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
    public bool isUIObjectInteractable(InventoryItem ObjectDropped, InventoryItem ObjectInInventorySlot) 
    {
        //Debug.Log($"{ObjectDropped.objectName} dropped onto {ObjectInInventorySlot.objectName}");
        ObjectCombinationInInventory itemCombination = ObjectDropped.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(ObjectInInventorySlot.tagInfo.objectName)) 
        {
            return itemCombination.CheckForCombination(ObjectDropped, ObjectInInventorySlot);
        }
        itemCombination = ObjectInInventorySlot.GetComponent<ObjectCombinationInInventory>();
        if (itemCombination != null && itemCombination.keyValuePairs.ContainsKey(ObjectDropped.tagInfo.objectName))
        {
                return itemCombination.CheckForCombination(ObjectInInventorySlot, ObjectDropped);
        }
        return false;
    }
}
