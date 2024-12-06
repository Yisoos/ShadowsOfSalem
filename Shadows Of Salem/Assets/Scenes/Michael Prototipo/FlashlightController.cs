using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; // Componente de luz de la linterna
    public float lightDuration = 10f; // Duración de la linterna en segundos
    public Button flashlightButton; // Botón para activar/desactivar la linterna

    private bool isLightOn = false;
    private Coroutine lightCoroutine;

    void Start()
    {
        // Asegurarse de que la linterna esté apagada al inicio
        flashlight.enabled = false;
        // Agregar el listener al botón
        flashlightButton.onClick.AddListener(ToggleFlashlight);
    }

    void ToggleFlashlight()
    {
        if (isLightOn)
        {
            // Apagar la linterna si está encendida
            if (lightCoroutine != null)
            {
                StopCoroutine(lightCoroutine);
            }
            flashlight.enabled = false;
            isLightOn = false;
        }
        else
        {
            // Encender la linterna si está apagada
            lightCoroutine = StartCoroutine(FlashlightTimer());
        }
    }

    IEnumerator FlashlightTimer()
    {
        flashlight.enabled = true;
        isLightOn = true;

        yield return new WaitForSeconds(lightDuration);

        flashlight.enabled = false;
        isLightOn = false;
    }
}
