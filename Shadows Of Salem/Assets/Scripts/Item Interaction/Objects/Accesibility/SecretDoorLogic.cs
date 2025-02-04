using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class SecretDoorLogic : MonoBehaviour
{
    public float defaultRotation; // Ángulo de rotación inicial del dial
    public float endRotation; // Ángulo de rotación inicial del dial
    [Space(10), Range(0, 15)] public float ReturnRotationSpeed;
    public string[] Solucion;
    public InterchangableItemPlacement[] Botones;
    public Transform[] doorStates;
    private float startAngle;
    private float previousAngle; // Ángulo previo al girar
    private float currentAngle; // Ángulo actual durante el giro
    private bool isReturning; // Indicador de si el dial está volviendo a su posición inicial

    private void Start()
    {
        doorStates[0].gameObject.SetActive(false);
        doorStates[1].gameObject.SetActive(true);
    }
    public bool isSolved() 
    {
        for (int i = 0; i < Solucion.Length; i++)
        {
            if (Solucion[i] != Botones[i].thisTag.objectName)
            {
                return false;
            }
        }
        return true;
    }
    public void OnMouseDown()
    {
        Vector3 origin = transform.position;
        Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));

        startAngle = GetAngleBetweenPoints(origin, pointer);
        previousAngle = startAngle;
    }

    public void OnMouseDrag()
    {
        if (isSolved()) 
        {
            if (!isReturning)
            {
                //Debug.Log($"{previousAngle}");

                Vector3 origin = transform.position;
                Vector3 pointer = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

                currentAngle = GetAngleBetweenPoints(origin, pointer);
                float rotateDirection = currentAngle - previousAngle;

                if (rotateDirection < 0 && rotateDirection > -50 && Mathf.Abs( transform.rotation.eulerAngles.z - endRotation) > 5f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateDirection/2);
                }

                previousAngle = currentAngle;
            }
        }
    }

    void OnMouseUp()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.z - endRotation) <= 5f)
        {
            Debug.Log("PuertaAbierta");
            doorStates[0].gameObject.SetActive(true);
            doorStates[1].gameObject.SetActive(false);
        }
        else
        {
        StartCoroutine(ReturnDialPosition());
        }
    }
    private IEnumerator ReturnDialPosition()
    {
        isReturning = true;

        float currentRotation = transform.rotation.eulerAngles.z;
        float targetRotation = defaultRotation;
        float step = ReturnRotationSpeed * Time.deltaTime * 20; // Velocidad de retorno por frame

        while (Mathf.Abs(currentRotation - targetRotation) > 0.1f)
        {
            currentRotation = currentRotation < targetRotation ? Mathf.MoveTowards(currentRotation, targetRotation, step) : Mathf.MoveTowards(currentRotation - 360, targetRotation, step);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
        isReturning = false;
    }
    float GetAngleBetweenPoints(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Normalize the angle to be within 0 to 360 degrees
        if (angle < 0) angle += 360f;

        return angle;
    }
}
