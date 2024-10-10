using UnityEngine;

public class Lock : MonoBehaviour
{
    public int lockID; // The ID of this lock
    public bool isCombinationLock;
    public int combination; // The key that unlocks this lock

    public void TryUnlock(Key key)
    {
        if (key != null && key.keyID==lockID)
        {
            Debug.Log("Lock opened!");
            Destroy(gameObject);
            Destroy(key.gameObject);
        }
        else
        {
            Debug.Log("Key does not match the required key.");
        }
    }
}
