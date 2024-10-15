using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenas : MonoBehaviour
{
    public string nombreDeEscena;

    void Update()
    {
        // Si el jugador presiona la barra espaciadora o hace clic en cualquier parte de la pantalla
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CambiarEscena();
        }
    }

    public void CambiarEscena()
    {
        Debug.Log("Cambiar Escena");
        SceneManager.LoadScene(nombreDeEscena);

    }
    
}
