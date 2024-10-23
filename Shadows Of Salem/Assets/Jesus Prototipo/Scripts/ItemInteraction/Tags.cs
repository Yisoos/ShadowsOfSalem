using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Lock,
    Key,
    Tool,
    Compartment,
    Inspectable,
    PopUpWindow,
    Other,
    Reusable
}

public class Tags : MonoBehaviour
{
    [Header("General Metadata")]
    public string objectName;
    public string objectDescription;
    public Sprite sprite;
    public int quantity;
    public ObjectType objectType;
}
