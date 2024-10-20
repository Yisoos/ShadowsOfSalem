using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour
{
    TMP_Text _tmpProText;
    string originalText; //  Almacena el texto original del componente TMP antes de que se inicie el efecto

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    [SerializeField] GameObject flechita;
    private bool isTyping = false;

    public CambiarEscenas changeScenes;

    void Start()
    {
        _tmpProText = GetComponent<TMP_Text>();

        if (_tmpProText != null)
        {
            originalText = _tmpProText.text; // Guarda el texto original en "writer"
            _tmpProText.text = ""; // Limpia el texto del componente TMP_Text

            if (flechita != null)
            {
                flechita.SetActive(false);
            }

            changeScenes = FindObjectOfType<CambiarEscenas>();

            StartCoroutine("TypeWriterTMP"); // Inicia la corrutina para realizar el efecto de m�quina de escribir
        }
    }

    IEnumerator TypeWriterTMP()
    {
        isTyping = true;

        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : ""; // mostrar o no el leadingchar

        yield return new WaitForSeconds(delayBeforeStart); // // Espera el tiempo especificado antes de comenzar a escribir

        foreach (char c in originalText)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length); // Si hay alg�n texto en pantalla, primero quita el leadingChar temporalmente
            }
            _tmpProText.text += c; // A�ade el siguiente car�cter
            _tmpProText.text += leadingChar; // Vuelve a a�adir el leadingChar para dar la ilusi�n de que el cursor sigue parpadeando o movi�ndose despu�s de cada car�cter
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (flechita != null)
        {
            flechita.SetActive(true); // Activa el GameObject
        }

    }
    void Update()
    {
        // Comprobar si el jugador intenta avanzar mientras el texto a�n est� siendo escrito
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
            {
                StopAllCoroutines(); 
                _tmpProText.text = originalText; // el texto actual igual al original 
                isTyping = false; // efecto terminado

                // Activar flechita ya que se ha saltado el efecto typewriter
                if (flechita != null)
                {
                    flechita.SetActive(true);
                }
            }
            return; 
        }

        if (changeScenes != null)
        {
            changeScenes.CambiarEscena(); // Call the method to check if the scene should change
        }
    }

   
}
