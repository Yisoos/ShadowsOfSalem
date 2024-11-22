using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangeTrigger : MonoBehaviour
{
    public int viewNumber;
    public SceneNavigator nav;

    private void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform))
        nav.ChangeViewRoom(viewNumber);
    }
}
