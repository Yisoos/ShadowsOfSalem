using UnityEngine;

public class Lock : MonoBehaviour
{
    public int lockID; // The ID of this lock
    public bool isLocked;
    public bool isPhysicalLock;
    public bool isCombinationLock;
    public int combination; // The key that unlocks this lock
    public InventoryOrder inventoryOrder;

    public void TryUnlock(Key key)
    {
        if (key != null && key.keyID==lockID && isLocked)
        {
            Debug.Log("Lock opened!");
            isLocked = false;
            if(isPhysicalLock) 
            { 
                gameObject.SetActive(false); 
            }
            Tags keyTag = key.gameObject.GetComponent<Tags>();
            inventoryOrder.DeleteItem(keyTag);
        }
        else
        {
            Debug.Log("Key does not match the required key.");
        }
    }
}
