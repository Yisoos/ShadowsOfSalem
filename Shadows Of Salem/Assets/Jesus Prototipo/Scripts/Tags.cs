using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
    {
        Lock,
        Key,
        CloseUp,
        other,
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
    public bool isCollectible;
    
    
}
