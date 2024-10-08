using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
    {
        Lock,
        Key,
        DetailedView,
        Inspectable,
        PopUpWindow,
        Other,
    }
public class Tags : MonoBehaviour
{
    [Header("General Tags")]
    public string objectName;
    public ObjectType objectType;
    [Header("Lock Tags")]
    public bool isLocked;
    public int keyID;
    public int unlockCombination;
    [Header("Inventory Tags")]
    [Header("Pop-Up Tags")]
    public GameObject referenceItem;
    [Header("Dependencies")]
    public bool lockDependency;
    public GameObject requiredLock;
    public string[] dependency;
}
