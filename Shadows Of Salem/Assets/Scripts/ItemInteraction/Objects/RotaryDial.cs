using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform dialTransform; // Assign the dial UI element here
    public float minRotationAngle = 0f;  // Minimum angle for rotation
    public float maxRotationAngle = 150f; // Maximum angle for rotation
    public float returnSpeed = 200f;      // Speed at which the dial returns to original position

    private float initialRotationZ;       // The initial rotation angle of the dial
    private Vector2 previousDragPosition; // The previous position of the drag
    private bool isDragging = false;
    private bool isReturning = false;     // To track if the dial is returning to its original position

    private void Start()
    {
        if (dialTransform == null)
            dialTransform = GetComponent<RectTransform>();

        // Set the initial rotation based on the current Z rotation
        initialRotationZ = dialTransform.eulerAngles.z;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Only allow dragging if not currently returning to the original position
        if (isReturning) return;

        // Start tracking the initial drag position
        previousDragPosition = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || isReturning) return;

        // Get the current and previous positions of the drag relative to the dial center
        Vector2 dialCenter = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, dialTransform.position);
        Vector2 currentDragPosition = eventData.position - dialCenter;
        Vector2 previousPosition = previousDragPosition - dialCenter;

        // Calculate the angle difference between the previous and current drag positions
        float angleDifference = Vector2.SignedAngle(previousPosition, currentDragPosition);
        Debug.Log(angleDifference);

        // Only allow clockwise rotation by ignoring counterclockwise movement
        //if (angleDifference > 0)
        {
            // Calculate the new rotation and clamp it to min and max rotation limits
            float newRotationZ = Mathf.Clamp(dialTransform.eulerAngles.z + angleDifference, minRotationAngle, maxRotationAngle);

            // Apply the calculated rotation to the dial
            dialTransform.eulerAngles = new Vector3(0, 0, newRotationZ);
        }

        // Update previous drag position for the next frame
        previousDragPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Start the coroutine to reset the rotation to the initial position
        if (!isReturning)
            StartCoroutine(ReturnToOriginalPositionCounterClockwise());
    }

    private IEnumerator ReturnToOriginalPositionCounterClockwise()
    {
        isReturning = true;

        // Smoothly rotate back to the initial rotation counterclockwise only
        float currentRotationZ = dialTransform.eulerAngles.z;

        // Ensure the rotation goes counterclockwise to return to the initial rotation
        while (Mathf.Abs(Mathf.DeltaAngle(currentRotationZ, initialRotationZ)) > 0.1f)
        {
            // Calculate the step rotation based on returnSpeed, forcing counterclockwise rotation
            float step = -returnSpeed * Time.deltaTime;

            // Apply the counterclockwise rotation using Mathf.MoveTowardsAngle
            currentRotationZ = Mathf.MoveTowardsAngle(currentRotationZ, initialRotationZ, -step);

            // Apply the calculated rotation
            dialTransform.eulerAngles = new Vector3(0, 0, currentRotationZ);

            yield return null; // Wait until the next frame
        }

        // Ensure the rotation is set exactly to the initial rotation when done
        dialTransform.eulerAngles = new Vector3(0, 0, initialRotationZ);

        isReturning = false; // Allow dragging again after reset
    }
}
