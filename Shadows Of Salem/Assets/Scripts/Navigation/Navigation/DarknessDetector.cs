using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessDetector : MonoBehaviour
{
    public Transform[] viewVersions;
    public string[] requiredItem;
    public string[] displayText;
    public float waitTime;
    public string wind;
    InventoryItem objectToChange;
    bool lanternLit;
    private Coroutine blownLightOffCoroutine; // Store the coroutine reference

    private void Awake()
    {
        lanternLit = false;
    }

    private void OnEnable()
    {
        LightsOut();
        CheckForLight();
    }

    public void LightsOut()
    {
        viewVersions[0].gameObject.SetActive(true);
        viewVersions[1].gameObject.SetActive(false);
        viewVersions[2].gameObject.SetActive(false);
    }

    public void LightsOn()
    {
        viewVersions[0].gameObject.SetActive(false);
        viewVersions[1].gameObject.SetActive(false);
        viewVersions[2].gameObject.SetActive(true);
    }

    public void LightBlownOff()
    {
        viewVersions[0].gameObject.SetActive(false);
        viewVersions[1].gameObject.SetActive(true);
        viewVersions[2].gameObject.SetActive(false);

        // Start the coroutine and store its reference
        if (blownLightOffCoroutine != null)
        {
            StopCoroutine(blownLightOffCoroutine);
        }
        blownLightOffCoroutine = StartCoroutine(BlownLightOff());
    }

    public void CheckForLight()
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();
        FeedbackTextController feedbackText = FindAnyObjectByType<FeedbackTextController>();

        if (inventory.items.Exists(item => item.itemTag.objectName == requiredItem[requiredItem.Length - 1]))
        {
            LightsOn();
            if (!lanternLit)
            {
                // Stop the coroutine if it's running
                if (blownLightOffCoroutine != null)
                {
                    StopCoroutine(blownLightOffCoroutine);
                    blownLightOffCoroutine = null; // Clear the reference

                }
                if (transform.gameObject.activeSelf)
                {
                    feedbackText.PopUpText(displayText[displayText.Length - 1]);
                }
                    lanternLit = true;
            }
            return;
        }
        else
        {
            for (int i = 0; i < requiredItem.Length; i++)
            {
                objectToChange = inventory.items.Find(item => item.itemTag.objectName == requiredItem[i]);
                if (objectToChange != null)
                {
                    LightBlownOff();
                    return;
                }
            }
            LightsOut();
            if (transform.gameObject.activeSelf)
            {
            feedbackText.PopUpText(displayText[0]);

            }
        }
    }

    private IEnumerator BlownLightOff()
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();
        FeedbackTextController feedbackText = FindAnyObjectByType<FeedbackTextController>();
        ObjectCombinationInInventory combination = objectToChange.GetComponent<ObjectCombinationInInventory>();

        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);
        if (transform.gameObject.activeSelf)
        {
            feedbackText.PopUpText(displayText[1]);
        }
        int index = combination.keyValuePairs[wind];
        inventory.ChangeItemStatus(objectToChange, combination.objetosCombinables[index].newItemStatus, combination.objetosCombinables[index].newItemStatusSprite);
        // Deactivate the object
        LightsOut();
    }
}
