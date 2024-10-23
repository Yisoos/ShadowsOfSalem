using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public GameObject[] inventorySlot;
    public bool itemCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CollectItem(GameObject itemPrefab) 
    {
        Debug.Log("CollectItem function ativated");
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].transform.childCount == 0)
            {

                GameObject item = Instantiate(itemPrefab, inventorySlot[i].transform);
                
                Debug.Log("item collected");
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
