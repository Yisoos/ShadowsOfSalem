using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tags))]
public class MultipleViewItem : MonoBehaviour
{
    [Tooltip("Sprites de los diferentes estados del objeto")] public Sprite[] objectStatusSprite;
    [Tooltip("Nombre en \"Tags \" de los diferentes estados del objeto")] public string[] objectStatusName;
    [Tooltip("Lista representativa del mismo objeto visto en otras vistas")]public Transform[] objectVariations;
    public int currentStatus;
    private void Awake()
    {
        Tags thisTag = GetComponent<Tags>();
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        thisTag.objectName = objectStatusName[currentStatus];
        thisTag.sprite = objectStatusSprite[currentStatus];
        thisSprite.sprite = objectStatusSprite[currentStatus];
        UpdateMultipleViews(currentStatus);
    }
    public void HideObjectsInAllViews() 
    {
        gameObject.SetActive(false);
        for (int i = 0; i < objectVariations.Length; i++) 
        {
            objectVariations[i].gameObject.SetActive(false);
        }
    }
    public void UpdateMultipleViews(int index) 
    {
        for (int i = 0; i < objectVariations.Length; i++)
        {
            MultipleViewItem differentViewAppearence = objectVariations[i].GetComponent<MultipleViewItem>();
            Tags differentViewTag = objectVariations[i].GetComponent<Tags>();
            SpriteRenderer differentViewSprite = objectVariations[i].GetComponent<SpriteRenderer>();    
            if (differentViewAppearence != null && (differentViewAppearence.objectStatusSprite.Length > 0 || differentViewAppearence.objectStatusName.Length > 0)) 
            {
                differentViewTag.objectName = differentViewAppearence.objectStatusName[index];
                differentViewTag.sprite = differentViewAppearence.objectStatusSprite[index];
                differentViewSprite.sprite = differentViewAppearence.objectStatusSprite[index];
            }
            else 
            {
                differentViewSprite.sprite = objectStatusSprite[index];
                if (differentViewTag!= null) 
                {
                    differentViewTag.objectName = objectStatusName[index];
                    differentViewTag.sprite = objectStatusSprite[index];
                }
            }
        }
    }
}
