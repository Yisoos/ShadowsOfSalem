using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepReusableItem : MonoBehaviour
{
    public List<GameObject> reusableInventoryItems;
    [Space(10)] public InventoryItem itemPrefab;
    private Inventory inventory;

    void Awake()
    {
        LoadReusableInventoryItem();
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        inventory = FindAnyObjectByType<Inventory>();
        if(inventory!= null)
        {
            LoadReusableInventoryItem();
        }
    }
    public void LoadReusableInventoryItem()
    {

        foreach(var reusableItem in reusableInventoryItems)
        {
            ItemCollection inheretedTag = reusableItem.GetComponent<ItemCollection>();
            inventory.CollectItem(itemPrefab, inheretedTag.inheritTags, inheretedTag.amountToCollect);
        }
    }

    
    public void UpdateReusableItemList(Tags origin)
    {
        if (origin.objectType == TypeObject.Reusable && !reusableInventoryItems.Find(obj => obj.transform == origin.transform))
        { 
            if (origin == null) return;

            if(origin.transform != null)
            {
                GameObject originalObject = origin.transform.gameObject;

                // Option 2: Clone the object and store the copy
                
                DontDestroyOnLoad(originalObject);
                originalObject.transform.SetParent(transform);
                reusableInventoryItems.Add(originalObject);

                Debug.Log($"Added {originalObject.name} to the reusable inventory list.");
            }
        }
    }

    private GameObject CloneGameObjectWithComponents(GameObject original)
    {
        GameObject clone = new GameObject(original.name + "_Clone");

        // Copy all components from original to clone
        foreach (Component component in original.GetComponents<Component>())
        {
            System.Type componentType = component.GetType();
            Component copiedComponent = clone.AddComponent(componentType);

            // Copy values from the original component
            foreach (var field in componentType.GetFields())
            {
                field.SetValue(copiedComponent, field.GetValue(component));
            }

            foreach (var property in componentType.GetProperties())
            {
                if (property.CanWrite)
                {
                    try
                    {
                        property.SetValue(copiedComponent, property.GetValue(component));
                    }
                    catch { } // Ignore properties that throw errors when copying
                }
            }
        }

        return clone;
    }
}
