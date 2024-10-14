using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;

// Custom editor for the lock (Lock) script
[CustomEditor(typeof(Lock))]
public class CustomInspectorLock : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the Tags component
        Lock lockScript = (Lock)target;
        EditorGUILayout.LabelField("Lock Type", EditorStyles.boldLabel);
        lockScript.isCombinationLock = EditorGUILayout.Toggle("Is Combination Lock", lockScript.isCombinationLock);
        if(lockScript.isCombinationLock)
        {
            lockScript.combination = EditorGUILayout.IntField("Lock Combination", lockScript.combination);
        }
        else
        {
            lockScript.lockID = EditorGUILayout.IntField("Key ID", lockScript.lockID);
            lockScript.isPhysicalLock = EditorGUILayout.Toggle("Is Physical lock", lockScript.isPhysicalLock);
        }
        lockScript.isLocked = EditorGUILayout.Toggle("Is Locked", lockScript.isLocked);
        lockScript.inventoryOrder = (InventoryOrder)EditorGUILayout.ObjectField("Inventory Order", lockScript.inventoryOrder, typeof(InventoryOrder), allowSceneObjects: true);

    }
}
