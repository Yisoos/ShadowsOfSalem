using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

// Clase RotaryDial para simular el comportamiento de un dial giratorio
public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float defaultRotation; // �ngulo de rotaci�n inicial del dial
    public float endRotation; // �ngulo de rotaci�n inicial del dial
    public TMP_Text phoneNumberDisplay;
    public RotaryDialControl phoneParent;
    public Transform popUp;

    private string currentNumber; // N�mero actual que se obtiene al girar el dial
    private float startAngle;
    private float previousAngle; // �ngulo previo al girar
    private float currentAngle; // �ngulo actual durante el giro
    private bool isReturning; // Indicador de si el dial est� volviendo a su posici�n inicial
    private float DistanceToEnd;
    private Animator animator;

    private void Start()
    {
        ResetDial();
        animator= GetComponentInParent<Animator>();
        if (animator != null)
        {
            animator.SetBool("ToggleTutorial", true);
        }
    }
    private void OnEnable()
    {
        ResetDial();
        if (animator != null)
        { 
            animator.SetBool("ToggleTutorial", true);
        }
    }

    public void ResetDial() 
    {
        if (phoneParent!= null && phoneParent.UIInventoryDisplay != null)
        {
            phoneParent.UIInventoryDisplay.gameObject.SetActive(false);
        }
        if (phoneParent != null && phoneParent.feedbackText != null ) 
        {
            phoneParent.feedbackText.PopUpText("");
            phoneParent.feedbackText.gameObject.SetActive(false);
            phoneParent.isDialOpened = true;
        }
        phoneNumberDisplay.text = string.Empty;
        transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
    }

    private void OnDisable()
    {
        if (animator != null)
        {
            animator.SetBool("ToggleTutorial", false);
        }
        StopCoroutine(TutorialAnimationTimeOut());

        if (phoneParent.UIInventoryDisplay != null)
        {
            phoneParent.UIInventoryDisplay.gameObject.SetActive(true);
        }
        if (phoneParent.feedbackText != null)
        {
            phoneParent.feedbackText.gameObject.SetActive(true);
        }
        phoneParent.isDialOpened = false;
    }
    // Calcula el �ngulo entre dos puntos en la pantalla
    float GetAngleBetweenPoints(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Normalize the angle to be within 0 to 360 degrees
        if (angle < 0) angle += 360f;

        return angle;
    }

    private TMP_Text GetDigit(PointerEventData eventData)
    {
        // Create a new PointerEventData instance for the event
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        // List to store the raycast results
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast to find all UI objects under the pointer
        EventSystem.current.RaycastAll(pointerEventData, results);

        // Loop through the results and check for TMP_Text components
        foreach (var result in results)
        {
            // Check if the RaycastResult's GameObject has a TMP_Text component
            TMP_Text tmpTextComponent = result.gameObject.GetComponent<TMP_Text>();

            if (tmpTextComponent != null)
            {
                // Log the name of the UI element containing TMP_Text
                //Debug.Log("UI Object with TMP_Text hit: " + result.gameObject.name);

                // Return the TMP_Text component
                return tmpTextComponent;
            }
        }

        // Return null if no UI object with TMP_Text is found
        return null;
    }

    // Evento que se ejecuta al iniciar el arrastre del dial
    public void OnBeginDrag(PointerEventData eventData)
    {
        StopCoroutine(TutorialAnimationTimeOut());
        if (animator != null)
        {
            animator.SetBool("ToggleTutorial", false);
        }
            Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
       
        startAngle = GetAngleBetweenPoints(origin, pointer);
        previousAngle = startAngle;
        DistanceToEnd = startAngle < endRotation? startAngle + (360 - endRotation) : startAngle-endRotation;
        if (eventData != null && GetDigit(eventData) != null)
        {
        currentNumber = GetDigit(eventData).text;
        }
    }

    // Evento que se ejecuta mientras se arrastra el dial
    public void OnDrag(PointerEventData eventData)
    {
        if (!isReturning)
        {
            //Debug.Log($"{previousAngle}");

            Vector3 origin = transform.position;
            Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

            currentAngle = GetAngleBetweenPoints(origin, pointer);
            float rotateDirection = currentAngle - previousAngle;

            if (rotateDirection < 0 && rotateDirection > -50 && DistanceToEnd > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateDirection);
                DistanceToEnd = DistanceToEnd + rotateDirection >= -50 ? DistanceToEnd + rotateDirection : DistanceToEnd;
               // currentDistanceToEnd += rotateDirection;
                //Debug.Log(rotateDirection);
            }

            previousAngle = currentAngle;
        }
    }

    // Evento que se ejecuta al finalizar el arrastre del dial
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DistanceToEnd <= 5)
        {
            // Only add the number if rotation has completed
            if (phoneNumberDisplay != null)
            {
                if (phoneNumberDisplay.text.Length >= phoneParent.numberToCall.Length)
                {
                        phoneNumberDisplay.text = string.Empty;
                }
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    phoneNumberDisplay.text += string.IsNullOrEmpty(phoneNumberDisplay.text) ? currentNumber : $"-{currentNumber}";
                    phoneNumberDisplay.font = phoneParent.fontAsset[0];
                }
                if (phoneNumberDisplay.text == phoneParent.numberToCall)
                {
                    phoneNumberDisplay.font = phoneParent.fontAsset[1];
                    phoneNumberDisplay.text = phoneParent.displayText[0];
                    phoneParent.correctNumberCalled = true;
                }
            }
        }
        currentNumber = string.Empty;
        StartCoroutine(ReturnDialPosition());
    }

    // Corrutina que devuelve el dial a su posici�n inicial
    private IEnumerator ReturnDialPosition()
    {
        WinLevel winLevel = FindAnyObjectByType<WinLevel>();
      
        isReturning = true;

        float currentRotation = transform.rotation.eulerAngles.z;
        float targetRotation = defaultRotation;
        float step = phoneParent.dialReturnSpeed * Time.deltaTime * 20; // Velocidad de retorno por frame

        while (Mathf.Abs(currentRotation - targetRotation) > 0.1f)
        {
            currentRotation = currentRotation < targetRotation ? Mathf.MoveTowards(currentRotation, targetRotation, step) : Mathf.MoveTowards(currentRotation - 360, targetRotation, step);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        if(phoneParent.correctNumberCalled)
        winLevel.TryToWinLevel();

        transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
        isReturning = false;
        StartCoroutine(TutorialAnimationTimeOut());
    }

    public void DesactivarPopUp() 
    {
        if (!isMouseOnDial()) 
        {
            OnDisable();
            transform.parent.parent.gameObject.SetActive(false);
        }
    }

    public bool isMouseOnDial()
    {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the direct parent of this UI element
            Transform directParent = transform.parent;

            // If there is no parent, return false
            if (directParent == null)
                return true;

            // Prepare PointerEventData to store the current mouse position
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // Store only the topmost UI element under the mouse position
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            // Check if the topmost element is the direct parent
            if (raycastResults.Count > 0)
            {
                // The first element in raycastResults is the topmost UI element
                if (raycastResults[0].gameObject == directParent.gameObject)
                {
                    return false;
                }
            }
        }

        // Return false if the topmost UI element is not the direct parent
        return false;
    }
    private IEnumerator TutorialAnimationTimeOut()
    {
        yield return new WaitForSeconds(phoneParent.RotaryTutorialAnimationTimeOut);
        if (animator != null)
        {
            animator.SetBool("ToggleTutorial", true);
        }
    }
}
