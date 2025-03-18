using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolucion_Logica : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private Resolution[] resolutions;

    void Start()
    {
        CheckResolution();

        // Agrega un listener al Dropdown para reproducir sonido cuando se selecciona una opción
        dropdown.onValueChanged.AddListener(OnDropdownClick);
    }

    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        // Agrega opciones al dropdown y busca la resolución actual
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if ((Screen.currentResolution.height == resolutions[i].height) &&
                (Screen.currentResolution.width == resolutions[i].width))
            {
                currentResolution = i;
            }
        }

        dropdown.AddOptions(options);
        dropdown.value = PlayerPrefs.GetInt("Resolution", currentResolution);
        dropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Resolution newResolution = resolutions[resolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex); // Guarda la resolución seleccionada
    }

    // Método que se llama cuando se hace clic en el Dropdown
    private void OnDropdownClick(int value)
    {
        SoundManager.Instance.PlaySound("Click", SoundType.UI);

    }
}
