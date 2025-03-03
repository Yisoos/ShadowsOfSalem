using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeObject
{
    Lock,
    Key,
    Tool,
    Compartment,
    Inspectable,
    Reusable
}
[System.Serializable]
public class Tags 
{
    public string objectName;
    [TextArea(1, 10), Tooltip("Array de mensajes: Generalmente, los primeros Elementos contienen los textos del objeto 'no accesible', y el ultimo Elemento contiene el mensaje de 'accesible'.")]
    public string[] displayText;
    public Sprite sprite;
    public bool stackable;
    public TypeObject objectType;

    [HideInInspector] public int quantity;
    public Transform transform;
}
