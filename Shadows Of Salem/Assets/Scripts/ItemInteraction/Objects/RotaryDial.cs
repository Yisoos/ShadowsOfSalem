using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float StartingRotation;
    public float EndRotation;
    [Range(0,60)]public float dialReturnSpeed;

    private Vector2 Current ;
    private void Start()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    private IEnumerator ReturnToOriginalPositionCounterClockwise()
    {
        return null;
    }
}
