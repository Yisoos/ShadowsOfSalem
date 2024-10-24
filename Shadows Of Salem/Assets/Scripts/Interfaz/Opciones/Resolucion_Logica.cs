using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolucion_Logica : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    Resolution[] resolution;
    
    // Start is called before the first frame update
    void Start()
    {
        CheckResolution();
    }
    public void CheckResolution()
    {
        resolution = Screen.resolutions;
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        // Populate the options list and find the current resolution
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + "x" + resolution[i].height;
            options.Add(option);  // Add options to the dropdown
            if ((Screen.currentResolution.height == resolution[i].height) &&
                (Screen.currentResolution.width == resolution[i].width))
            {
                currentResolution = i;
            }
        }
        dropdown.AddOptions(options);
        dropdown.value = PlayerPrefs.GetInt("Resolution", currentResolution);
        dropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolutionIndicator)
    {
        Resolution newResolution = resolution[resolutionIndicator];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndicator); // Store selected resolution in PlayerPrefs
    }
}
