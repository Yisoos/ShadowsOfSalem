using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultipleViewDetails
{
    public Transform otherViewObject;
    [Tooltip("Sprites de los diferentes estados del objeto. Si es igual que la de este objeto, lo puedes dejar en blanco")] public Sprite[] ObjectStatusSprite;
    [Tooltip("Nombre en \"Tags \" de los diferentes estados del objeto. Si es igual que la de este objeto, lo puedes dejar en blanco")] public string[] ObjectStatusName;
}
