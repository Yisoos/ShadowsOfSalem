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
    public ObjectType objectType;
    public ObjectCategory category;

}
