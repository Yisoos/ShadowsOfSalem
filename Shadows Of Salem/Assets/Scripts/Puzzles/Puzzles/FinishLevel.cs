using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public string sceneToGoToWhenFinished;
    public void OnMouseDown()
    {
        CambiarEscenas scenesManager = FindAnyObjectByType<CambiarEscenas>();
        scenesManager.ChangeToScene(sceneToGoToWhenFinished);
    }
    
}
