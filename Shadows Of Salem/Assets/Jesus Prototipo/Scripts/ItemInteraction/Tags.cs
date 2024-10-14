using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Lock,
    Key,
    Compartment,
    Inspectable,
    PopUpWindow,
    Other
}

public enum ObjectCategory
{
    Tool,
    Clue,
    Decoration,
    Furniture
}

public class Tags : MonoBehaviour
{
    [Header("General Metadata")]
    public string objectName;
    public string objectDescription;
    public Sprite sprite;
    public int quantity;
    public ObjectType objectType;
    public ObjectCategory category;

}
