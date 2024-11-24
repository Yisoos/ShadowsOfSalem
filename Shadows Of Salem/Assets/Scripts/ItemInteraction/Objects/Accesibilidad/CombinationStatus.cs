using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CombinationStatus
{
    [Tooltip("Escribe aquí el nombre en \"Tags\" del objeto que quieres combinar")] public string itemToCombine;
    [Tooltip("Escribe aquí el nombre que debe tener este objeto en \"Tags\" para poder hacer la combinación")] public string currentItemStatus;
    [Tooltip("Añade aquí el sprite del objeto después de arrastrar el item combinable")] public Sprite newItemStatusSprite;
    [Tooltip("¿Cual será el nombre del objeto después de hacer la combinación?")] public string newItemStatus;
}
