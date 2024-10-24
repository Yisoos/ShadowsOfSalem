using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calidad_Logica : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int dropdownValue;

    // Start is called before the first frame update
    void Start()
    {
        dropdownValue = PlayerPrefs.GetInt("Quality", 3);
        QualitySettings.SetQualityLevel(dropdownValue);
        dropdown.value = dropdownValue;
    }
    public void ChangeQuality(int value)
    {
        dropdownValue = value;
        QualitySettings.SetQualityLevel(dropdownValue);
        PlayerPrefs.SetInt("Quality", value);
    }

}
