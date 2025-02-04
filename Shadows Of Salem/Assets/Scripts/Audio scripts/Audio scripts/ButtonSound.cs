using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound; // El clip de audio que se reproducirá
    private Button button;      // Referencia al componente Button

    private void Awake()
    {
        // Obtenemos el componente Button en el mismo GameObject
        button = GetComponent<Button>();

        // Agregamos el listener para reproducir el sonido al hacer clic
        button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            // Utilizamos el AudioManager para reproducir el sonido
            AudioManager.Instance.PlaySFX(clickSound);
        }
        else
        {
            Debug.LogWarning("No se asignó un sonido de clic en el botón.");
        }
    }
}
