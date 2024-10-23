using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaCompleta_Logica : MonoBehaviour
{
    //Declarando variables
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = Screen.fullScreen;
    }

    public void ChangeFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }

}
