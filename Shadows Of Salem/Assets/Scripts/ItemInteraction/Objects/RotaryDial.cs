using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

// Clase RotaryDial para simular el comportamiento de un dial giratorio
public class RotaryDial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float defaultRotation; // Ángulo de rotación inicial del dial
    public float endRotation; // Ángulo de rotación inicial del dial
    [Range(0, 15)] public float dialReturnSpeed; // Velocidad de retorno del dial a su posición inicial
    public string numberToCall; // Número de teléfono que se desea marcar
    public FeedbackTextController feedbackText;
    public TMP_Text phoneNumberDisplay;
    public Transform inventory;
    public RotaryDialControl phoneParent;
    private string currentNumber; // Número actual que se obtiene al girar el dial
    private float startAngle;
    private float previousAngle; // Ángulo previo al girar
    private float currentAngle; // Ángulo actual durante el giro
    private bool isReturning; // Indicador de si el dial está volviendo a su posición inicial
    private float distanceToEnd;
    private float currentDistanceToEnd;

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.gameObject.SetActive(false);
        }
        if (feedbackText != null ) 
        { 
            feedbackText.gameObject.SetActive(false);
        }
        phoneNumberDisplay.text = string.Empty;
    }
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.gameObject.SetActive(true);
        }
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true);

        }
    }
    // Calcula el ángulo entre dos puntos en la pantalla
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
        Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
       
        startAngle = GetAngleBetweenPoints(origin, pointer);
        previousAngle = startAngle;
        distanceToEnd = startAngle < endRotation? startAngle + (360 - endRotation) : startAngle-endRotation;
        Debug.Log(distanceToEnd);
        currentDistanceToEnd = distanceToEnd;
        currentNumber = GetDigit(eventData).text;
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

            if (rotateDirection < 0 && rotateDirection > -50 && currentDistanceToEnd > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateDirection);
                currentDistanceToEnd = currentDistanceToEnd + rotateDirection >= -50 ? currentDistanceToEnd + rotateDirection : currentDistanceToEnd;
               // currentDistanceToEnd += rotateDirection;
                Debug.Log(rotateDirection);
            }

            previousAngle = currentAngle;
        }
    }

    // Evento que se ejecuta al finalizar el arrastre del dial
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentDistanceToEnd <= 0)
        {
            // Only add the number if rotation has completed
            if (phoneNumberDisplay != null)
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    phoneNumberDisplay.text += string.IsNullOrEmpty(phoneNumberDisplay.text) ? currentNumber : $"-{currentNumber}";
                }
                if (phoneNumberDisplay.text.Length >= numberToCall.Length)
                {
                    if(phoneNumberDisplay.text == numberToCall) 
                    {
                        Debug.Log("Llamando al jefe...");
                    }
                    else
                    {
                        phoneNumberDisplay.text = string.Empty;
                    }
                }
            }
        }
        currentNumber = string.Empty;
        StartCoroutine(ReturnDialPosition());
    }

    // Corrutina que devuelve el dial a su posición inicial
    private IEnumerator ReturnDialPosition()
    {
        isReturning = true;

        float currentRotation = transform.rotation.eulerAngles.z;
        float targetRotation = defaultRotation;
        float step = dialReturnSpeed * Time.deltaTime * 20; // Velocidad de retorno por frame

        while (Mathf.Abs(currentRotation - targetRotation) > 0.1f)
        {
            currentRotation = currentRotation < targetRotation ? Mathf.MoveTowards(currentRotation, targetRotation, step) : Mathf.MoveTowards(currentRotation - 360, targetRotation, step);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
        isReturning = false;
    }

    [ContextMenu("Conectar componentes generales")]
    private void ConectarComponentesGenerales()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
    }
}
