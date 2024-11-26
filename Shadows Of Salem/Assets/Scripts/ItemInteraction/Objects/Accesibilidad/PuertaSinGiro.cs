using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class PuertaSinGiro : MonoBehaviour
{
    public string[] Solucion;
    public Tags[] Botones;
    public Transform[] doorStates;

    private void Start()
    {
        doorStates[0].gameObject.SetActive(true);
        doorStates[1].gameObject.SetActive(false);
    }
    public bool isSolved()
    {
        for (int i = 0; i < Solucion.Length; i++)
        {
            if (Solucion[i] != Botones[i].objectName)
            {
                return false;
            }
        }
        doorStates[0].gameObject.SetActive(false);
        doorStates[1].gameObject.SetActive(true);
        return true;
    }
    
}
