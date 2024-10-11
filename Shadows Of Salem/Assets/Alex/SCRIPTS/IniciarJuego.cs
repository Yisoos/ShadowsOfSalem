using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJuego : MonoBehaviour
{
    public void EmpezarJuego()
    {
        Debug.Log("Iniciar Juego");
        SceneManager.LoadScene("Nivel 0");

    }
    public void VolverAlMenu()
    {
        Debug.Log("Volver al Menu");
        SceneManager.LoadScene("Menu");

    }
}
