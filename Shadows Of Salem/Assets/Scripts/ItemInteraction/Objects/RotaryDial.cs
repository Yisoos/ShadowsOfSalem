using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float StartingRotation;
    public float EndRotation;
    [Range(0,60)]public float dialReturnSpeed;
    private Vector2 Current;
    private float previousAngle; 
    private float currentAngle;
    private float startRotation;

    private void Start()
    {
        startRotation = transform.rotation.eulerAngles.z;
    }

    float GetAngleBetweenPoints(Vector3 from, Vector3 to)
    {
        // Calculate direction vector from 'from' to 'to'
        Vector3 direction = to - from;

        // Get the currentAngle in radians, then convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Make sure currentAngle is always positive (0 to 360 degrees)
        if (angle < 0) angle += 360f;

        return angle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        previousAngle = GetAngleBetweenPoints(origin, pointer); ;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        currentAngle = GetAngleBetweenPoints(origin, pointer);
        Debug.Log("Angle between points: " + currentAngle + " degrees");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+(currentAngle - previousAngle));
        previousAngle = GetAngleBetweenPoints(origin, pointer); ;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(ReturnDialPosition());
    }

    private IEnumerator ReturnDialPosition()
    {
        return null;
    }
}
