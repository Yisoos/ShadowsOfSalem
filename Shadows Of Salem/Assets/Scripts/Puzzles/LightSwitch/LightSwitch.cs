using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool lightIsOn;
    public Transform darkOverlay;

    public Sprite onSprite;
    public Sprite offSprite;

    public bool accessibleState; 
    // Start is called before the first frame update
    void Start()
    {
        FlipSwitch(false);
    }

    private void OnMouseDown()
    {
        FlipSwitch(!lightIsOn);
    }

    public void FlipSwitch(bool state)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        lightIsOn = state;
        spriteRenderer.sprite= lightIsOn ? onSprite : offSprite;
        darkOverlay.gameObject.SetActive(state);
    } 

    public bool IsAccessible() 
    {
        return lightIsOn == accessibleState;
    }
}
