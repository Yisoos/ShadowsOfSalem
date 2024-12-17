using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangeTrigger : MonoBehaviour
{
    public int viewNumber;
    private SceneNavigator nav;

    private void Start()
    {
        nav = FindAnyObjectByType<SceneNavigator>();
    }
    private void OnMouseUp()
    {
        if(AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform))
        nav.ChangeViewRoom(viewNumber);
    }
}
