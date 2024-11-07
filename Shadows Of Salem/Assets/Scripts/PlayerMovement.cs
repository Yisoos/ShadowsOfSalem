using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    private Rigidbody rb;         // Referencia al Rigidbody del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Leer la entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D o flechas izquierda/derecha
        float verticalInput = Input.GetAxis("Vertical");      // W/S o flechas arriba/abajo

        // Calcular la dirección de movimiento en el espacio X y Y
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;

        // Calcular la nueva posición
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Mover al jugador usando MovePosition, lo que respeta las colisiones
        rb.MovePosition(newPosition);
    }
}
