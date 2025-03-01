using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleViewItem : MonoBehaviour
{
    [Tooltip("Sprites de los diferentes estados del objeto")] public Sprite[] objectStatusSprite;
    [Tooltip("Nombre en \"Tags \" de los diferentes estados del objeto")] public string[] objectStatusName;
    [Tooltip("Lista representativa del mismo objeto visto en otras vistas")]public Transform[] objectVariations;
    public int currentStatus;
    private void Awake()
    {
        ItemCollection itemCollection = GetComponent<ItemCollection>();
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        if (itemCollection != null)
        {
        itemCollection.inheritTags.objectName = objectStatusName[currentStatus];
        itemCollection.inheritTags.sprite = objectStatusSprite[currentStatus];
        }
        //thisSprite.sprite = objectStatusSprite[currentStatus];
        UpdateMultipleViews(currentStatus);
    }
    private void OnDisable()
    {
        UpdateAllViewsVisibility();
    }
    private void OnEnable()
    {
        UpdateAllViewsVisibility();
    }
    public void UpdateAllViewsVisibility() 
    {
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        bool active = gameObject.activeSelf;
        for (int i = 0; i < objectVariations.Length; i++) 
        {
            objectVariations[i].gameObject.SetActive(active);
        }
    }
    public void UpdateMultipleViews(int index) 
    {
        for (int i = 0; i < objectVariations.Length; i++)
        {
            MultipleViewItem differentViewAppearence = objectVariations[i].GetComponent<MultipleViewItem>();
            ItemCollection differentViewItemCollection = objectVariations[i].GetComponent<ItemCollection>();
            SpriteRenderer differentViewSprite = objectVariations[i].GetComponent<SpriteRenderer>();    
            if (differentViewAppearence != null && (differentViewAppearence.objectStatusSprite.Length > 0 || differentViewAppearence.objectStatusName.Length > 0)) 
            {
                if (differentViewItemCollection != null)
                {
                    differentViewItemCollection.inheritTags.objectName = differentViewAppearence.objectStatusName[index];
                    differentViewItemCollection.inheritTags.sprite = differentViewAppearence.objectStatusSprite[index];
                }
                    differentViewSprite.sprite = differentViewAppearence.objectStatusSprite[index];
            }
            else 
            {
                differentViewSprite.sprite = objectStatusSprite[index];
                if (differentViewItemCollection!= null) 
                {
                    differentViewItemCollection.inheritTags.objectName = objectStatusName[index];
                    differentViewItemCollection.inheritTags.sprite = objectStatusSprite[index];
                }
            }
        }
    }
}
