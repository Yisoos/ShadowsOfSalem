using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Optional: Highlight the button or provide visual feedback
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the button while dragging
        if (canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out position);
            rectTransform.localPosition = position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check for interaction with non-UI objects
        CheckForInteraction(eventData);
    }

    private void CheckForInteraction(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Tags targetTags = hit.collider.GetComponent<Tags>();
            // Check if the hit object has a specific tag or component
            if (targetTags != null)
            {
                // Example interaction: Print the tagName from the Tags script
                Debug.Log("Interacted with: " + hit.collider.gameObject.name + ", Tag: " + targetTags.objectType);

                // You can add additional logic based on the tagName
                // For example, if (targetTags.tagName == "SpecificTag") { ... }
            }
        }
    }
}
