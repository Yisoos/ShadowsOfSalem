using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DependencyHandler : MonoBehaviour
{
    [Header("Required Items")]
    public List<string> requiredItems; // List of items needed for this object (e.g., phone, cable, pliers)

    private HashSet<string> collectedItems = new HashSet<string>(); // Keep track of collected items

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
       
    }
}
