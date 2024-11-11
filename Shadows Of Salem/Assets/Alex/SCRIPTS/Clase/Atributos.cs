using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Atributos : MonoBehaviour
{
    [Header("Salud:")]
    [Space(10)]
    [Tooltip("Rango de salud del personaje.")]
    [ContextMenuItem("Restablecer salud del personaje", "RestablecerSaludMaxima")]
    [SerializeField, Range(0f, 10f)] private float salud;
    [HideInInspector] public float saludMaxima = 10f;
    [SerializeField, Range(0, 10)] private int vida;
    [HideInInspector] public int dano;

    

    [Header("Movimiento:")]
    [Space(10)]
    [SerializeField, Min(0.5f)] private float velocidad;
    [SerializeField] public int velocidadDeslizamiento;

    [Header("Equipamiento:")]
    [Space(10)]
    [SerializeField] private GameObject armaPrimaria;
    [SerializeField] private GameObject armaSecundaria;



    [Header("Personaje")]
    [TextArea(1,10)]
    [SerializeField] private string descripcion;

    [ContextMenu("RestarVida")]
    private void RestarVida()
    {
        vida--;
    }

    private void RestablecerSaludMaxima()
    {
        salud = saludMaxima;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
