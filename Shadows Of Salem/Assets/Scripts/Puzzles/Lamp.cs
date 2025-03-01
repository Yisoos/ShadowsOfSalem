using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Transform LampOn;
    public Transform LampOff;
    public bool isLightOn;
    // Start is called before the first frame update
    void Start()
    {
        ToggleLight(isLightOn);
    }

    private void OnMouseDown()
    {
        ToggleLight(isLightOn);
    }
    public void ToggleLight(bool state)
    {
        LampOn.gameObject.SetActive(state);
        LampOff.gameObject.SetActive(!state);
        isLightOn = !isLightOn;
    }
}
