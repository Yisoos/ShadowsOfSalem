using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tags))]
public class ObjectCombination : MonoBehaviour
{
    [SerializeField]CombinationStatus[] objetosCombinables;
    Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();

    private void Start()
    {
        for (int i = 0; i < objetosCombinables.Length; i++) // Replace with .Count if it's a List
        {
            var item = objetosCombinables[i];
            keyValuePairs[item.itemToCombine] = i; // Map itemToCombine to its index
        }
    }
    public bool CheckForCombination(Tags ObjectDropped)
    {
        Tags thisTag = GetComponent<Tags>();
        if (objetosCombinables[keyValuePairs[ObjectDropped.objectName]].currentItemName == thisTag.objectName)
        {
            SpriteRenderer itemImage = GetComponent<SpriteRenderer>();
            MultipleViewItem multipleViewChange = GetComponent<MultipleViewItem>();
            int index = keyValuePairs[ObjectDropped.objectName];
            itemImage.sprite = objetosCombinables[index].newItemStatusSprite;
            thisTag.objectName = objetosCombinables[index].newItemName;
            for (int i = 0; i < multipleViewChange.ObjectStatusSprite.Length; i++) 
            {
                if (multipleViewChange != null && itemImage.sprite == multipleViewChange.ObjectStatusSprite[i]) 
                {
                    multipleViewChange.ChangeObjectAppearance(i);
                }
            }
            Destroy(ObjectDropped.gameObject);
            return true;
        }

        return false;
    }
}
