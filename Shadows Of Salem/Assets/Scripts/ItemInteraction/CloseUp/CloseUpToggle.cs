using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class CloseUpToggle : MonoBehaviour
{
    public Transform vistaGeneral;
    public Collider2D[] objetosInspeccionables;
    public Transform[] closeUpObjetos;
    public SceneNavigator sceneNavigator;
    Transform currentCloseUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Detecta clic con botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0) && vistaGeneral.gameObject.activeSelf)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
            if (hit.collider != null && objetosInspeccionables.Contains(hit.collider))
            {
                // The hit object is in the objetosInspeccionables array
                Debug.Log("Hit an inspectable object!");
                SwitchView(hit.transform);
            }
        }
    }

    public void SwitchView(Transform closeUpReference) 
    {
        if(vistaGeneral.gameObject.activeSelf) 
        {
            closeUpReference.gameObject.SetActive(true);
            vistaGeneral.gameObject.SetActive(false);
            sceneNavigator.EnterCloseUpViewArrows();
            currentCloseUp = closeUpReference;

        }
        else if (currentCloseUp!=null && currentCloseUp.gameObject.activeSelf)
        {
            vistaGeneral.gameObject.SetActive(true);
            closeUpReference.gameObject.SetActive(false);
            sceneNavigator.LeaveCloseupViewArrows();
        }
        else 
        {
            vistaGeneral.gameObject.SetActive(true);
            for (int i = 0; i < closeUpObjetos.Length; i++) 
            {
                closeUpObjetos[i].gameObject.SetActive(false);
            }
            sceneNavigator.LeaveCloseupViewArrows();
        }
    }
}
