using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CombinationStatus
{
    [Tooltip("Escribe aqu� el nombre en \"Tags\" del objeto que quieres combinar")] public string itemToCombine;
    [Tooltip("Escribe aqu� el nombre que debe tener este objeto en \"Tags\" para poder hacer la combinaci�n")] public string currentItemStatus;
    [Tooltip("A�ade aqu� el sprite del objeto despu�s de arrastrar el item combinable")] public Sprite newItemStatusSprite;
    [Tooltip("�Cual ser� el nombre del objeto despu�s de hacer la combinaci�n?")] public string newItemStatus;
}
