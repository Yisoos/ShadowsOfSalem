using System.Collections.Generic;
using UnityEngine;

public class DependencyHandler : MonoBehaviour
{
    [Header("Required Items")]
    public List<GameObject> requiredItems; // List of items needed for this object (e.g., phone, cable, pliers)

    private HashSet<GameObject> collectedItems = new HashSet<GameObject>(); // Keep track of collected items

    public Sprite phoneWithCable; // The sprite when the cable is connected
    public Sprite phoneWithCableAndPliers; // The final sprite when both items are used
    private SpriteRenderer spriteRenderer; // Reference to change the sprite

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Check if the required item is part of the dependency list
    public void HandleItem(Tags droppedItem)
    {
        if (requiredItems.Contains(droppedItem.gameObject))
        {
            collectedItems.Add(droppedItem.gameObject);
            Tags itemTag = droppedItem.GetComponent<Tags>();

            // Check if the dropped item was a cable
            if (itemTag != null && itemTag.objectName == "Cable")
            {
                Debug.Log("Phone cable connected. Now find the pliers.");
                spriteRenderer.sprite = phoneWithCable; // Change sprite when cable is connected
            }
            // Check if the dropped item was pliers
            else if (itemTag != null && itemTag.objectName == "Alicates")
            {
                if (collectedItems.Contains(requiredItems.Find(i => i.GetComponent<Tags>().objectName == "Cable")))
                {
                    Debug.Log("Pliers used! Phone is now fully functional.");
                    spriteRenderer.sprite = phoneWithCableAndPliers; // Final sprite change
                    // Trigger phone activation logic here
                }
                else
                {
                    Debug.Log("You need the phone cable before using the pliers.");
                }
            }
        }
        else
        {
            Debug.Log("This item is not needed for the phone.");
        }
    }
}
