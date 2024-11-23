using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleViewItem : MonoBehaviour
{
    [Tooltip("Sprites de los diferentes estados del objeto")] public Sprite[] ObjectStatusSprite;
    [Tooltip("Nombre en \"Tags \" de los diferentes estados del objeto")] public string[] ObjectStatusName;
    [Tooltip("Lista representativa del mismo objeto visto en otras vistas")]public MultipleViewDetails[] objectVariations;
    public int currentStatus;
    private void Awake()
    {
        ChangeObjectAppearance(currentStatus);
    }
    public void HideObjectsInAllViews() 
    {
        gameObject.SetActive(false);
        for (int i = 0; i < objectVariations.Length; i++) 
        {
            objectVariations[i].otherViewObject.gameObject.SetActive(false);
        }
    }
    public void ChangeObjectAppearance(int index) 
    {
        currentStatus = index;
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        Tags thisTags = GetComponent<Tags>();
        if (thisTags != null)
        {
            thisTags.objectName = ObjectStatusName[index];
        }
        thisSprite.sprite = ObjectStatusSprite[index];
        for (int i = 0; i < objectVariations.Length; i++)
        {
            SpriteRenderer variationSprite = objectVariations[i].otherViewObject.GetComponent<SpriteRenderer>();
            Tags tags = objectVariations[i].otherViewObject.GetComponent<Tags>();
            variationSprite.sprite = objectVariations[i].ObjectStatusSprite[index] != null? objectVariations[i].ObjectStatusSprite[index] :ObjectStatusSprite[index];
            if (tags != null)
            {
                tags.objectName = objectVariations[i].ObjectStatusName[index]!= null ? objectVariations[i].ObjectStatusName[index] : ObjectStatusName[index];
            }
        }
    }
}
