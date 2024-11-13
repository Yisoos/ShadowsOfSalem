using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Globalization;

public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float defaultRotation;
    public float endRotation;
    [Range(0,15)]public float dialReturnSpeed;
    [SerializeField] private string numberToCall;
    [SerializeField] private string displayedNumber;

    private GameObject originalButton;
    private string currentNumber;
    private float previousAngle; 
    private float currentAngle;
    private float startAngle;
    private bool isReturning;
    private bool endRotationReached;

    private void Start()
    {
    }

    float GetAngleBetweenPoints(Vector3 from, Vector3 to)
    {
        // Calculate direction vector from 'from' to 'to'
        Vector3 direction = to - from;

        // Get the currentAngle in radians, then convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Make sure currentAngle is always positive (0 to 360 degrees)
        angle += 20f;
        if (angle < 20) angle += 360f;
        if (angle >360) angle -= 360f;
        return angle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the original button being dragged
        originalButton = eventData.pointerEnter;

        // Check if there is another button at this position
        GameObject overlappingButton = GetUIElementUnderPointer(eventData);
        TMP_Text digit = overlappingButton.GetComponent<TMP_Text>();
        if (overlappingButton != null && overlappingButton != originalButton && digit!=null)
        {
            currentNumber=digit.text;
            Debug.Log(digit.text);
        }
        else
        {
            Debug.Log("Started dragging without overlapping a different button.");
        Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        startAngle = GetAngleBetweenPoints(origin, pointer);
        previousAngle = startAngle;

        
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isReturning)
        {
            Vector3 origin = transform.position;
            Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            float currentRotation = transform.rotation.eulerAngles.z;
            currentAngle = GetAngleBetweenPoints(origin, pointer);
            float rotateDirection = currentAngle - previousAngle;

            if (Mathf.Abs(currentAngle - endRotation) <= 1f && Mathf.Abs(transform.rotation.z - endRotation ) >= 0.1f)
            {
                endRotationReached = true;
            }
                Debug.Log(currentAngle);
            if (currentAngle <= startAngle && currentAngle >= endRotation && !endRotationReached)
            {
                transform.rotation =  Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (rotateDirection));
                
            }
            previousAngle = GetAngleBetweenPoints(origin, pointer); 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(ReturnDialPosition());
        if (endRotationReached)
        {
            displayedNumber += displayedNumber == null ? currentNumber : "-" + currentNumber;
            Debug.Log(displayedNumber);
        }
        endRotationReached = false;
    }

    private IEnumerator ReturnDialPosition()
    {
        isReturning = true;

        float currentRotation = transform.rotation.eulerAngles.z;
        float targetRotation = defaultRotation;
        float step = dialReturnSpeed * Time.deltaTime*20; // Linear speed step per frame

        while (Mathf.Abs(currentRotation - targetRotation) > 0.1f)
        {
            // Move the current rotation closer to the target by a fixed amount
            currentRotation = currentRotation < targetRotation ? Mathf.MoveTowards(currentRotation, targetRotation, step) : Mathf.MoveTowards(currentRotation-360, targetRotation, step);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        // Snap exactly to starting rotation at the end
        transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
        isReturning = false;
    }

    private GameObject GetUIElementUnderPointer(PointerEventData eventData)
    {
        // Raycast to find UI element under pointer
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        // Find first UI element that is not the original button being dragged
        foreach (var result in results)
        {
            if (result.gameObject != originalButton)
            {
                return result.gameObject; // Found another UI element
            }
        }

        return null; // No overlapping UI element found
    }
}
