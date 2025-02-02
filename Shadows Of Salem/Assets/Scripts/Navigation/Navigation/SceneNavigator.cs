using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SceneNavigator : MonoBehaviour
{
    [Header ("Views")]
    [Tooltip("Arranstrar aqui todas las vistas generales que se van a cambiar con las flechas")]public GameObject[] view; //Las diferentes vistas guardadas en el inspector
    [Space(20)] public int startingView; //la vista donde empezará la escena
    [Space(20), Min(0)] public int[] roomEndViews;
    [HideInInspector] public int currentView; //la escena actual
    [Space(20)] int maxViews;
    [Header("Arrows")]
    public GameObject[] arrow; //flechas en la interfaz que se usan para moverse

    // Start is called before the first frame update
    void Start()
    {
        currentView = startingView;
        maxViews = view.Length-1;
        for (int i = 0; i < view.Length; i++) 
        {
            if ((currentView==i)) 
            {
                view[i].SetActive(true);
            }
            else
            {
                view[i].SetActive(false);
            }
        }
        if (arrow[2] != null)
        {
            Button backButton = arrow[2].GetComponent<Button>();
            // Add the function to the Button's OnClick event
            if (backButton != null)
            {
            backButton.onClick.AddListener(ExitCloseupView);

            }
            else
            {
                Debug.Log("Button Not found");
            }
        }
        else
        {
            Debug.Log("No back button");
        }
        //Debug.Log($"Current view:{currentView}");
        //Debug.Log($"Starting view:{startingView}");
        //Debug.Log($"Max view:{maxViews}");
        CheckViewEnd();
    }
    public void CheckViewEnd()
    {
        bool isLeftEnd = false;
        bool isRightEnd = false;

        if (roomEndViews != null)
        {
            // Check if currentView is at any of the end views
            for (int i = 0; i < roomEndViews.Length; i++)
            {
                if ( currentView == 0 || currentView == roomEndViews[i] + 1 )
                {
                    isLeftEnd = (currentView == 0 || currentView == roomEndViews[i] + 1);
                }
                if (currentView == maxViews || currentView == roomEndViews[i]) 
                {
                    isRightEnd = (currentView == maxViews || currentView == roomEndViews[i]);
                }
            }
        }

        // Update arrow visibility
        if (isLeftEnd && isRightEnd)
        {
            // Both ends reached
            arrow[0].SetActive(false);
            arrow[1].SetActive(false);
        }
        else if (isLeftEnd)
        {
            // Left end reached
            arrow[0].SetActive(false);
            arrow[1].SetActive(true);
        }
        else if (isRightEnd)
        {
            // Right end reached
            arrow[0].SetActive(true);
            arrow[1].SetActive(false);
        }
        else
        {
            // Not at any end
            arrow[0].SetActive(true);
            arrow[1].SetActive(true);
        }
    }

    public void ChangeViewRoom(int newView)
    {
            view[newView].SetActive(true);
            view[currentView].SetActive(false);
            currentView=newView;
            CheckViewEnd();
            //Debug.Log($"current view is {currentView}");
    }

    public void ChangeViewRight() 
    {
       if(currentView != maxViews) //si la vista actual no es el limite a la derecha, activa la escena a la derecha
        {
            view[currentView+1].SetActive(true);
            view[currentView].SetActive(false);
            currentView++;
            CheckViewEnd();
            //Debug.Log($"current view is {currentView}");
        }
    }
    public void ChangeViewLeft()
    {
        if (currentView != 0)//si la vista actual no es el limite a la izquierda activa la escena a la izquierda
        {
            view[currentView - 1].SetActive(true);
            view[currentView].SetActive(false);
            currentView--;
            CheckViewEnd();
           // Debug.Log($"current view is {currentView}");
        }
    }
    public void EnterCloseUpViewArrows()
    {
        arrow[0].SetActive(false);    // Activa la flecha izquierda
        arrow[1].SetActive(false);
        arrow[2].SetActive(true);
    }
    public void LeaveCloseupViewArrows()
    {
        arrow[2].SetActive(false);
        CheckViewEnd();
    }

    public void ExitCloseupView()
    {
        foreach(GameObject v in view)
        {
            CloseUpToggle closeUpToggle = v.GetComponent<CloseUpToggle>();
            if(closeUpToggle != null)
            {
                closeUpToggle.ExitCloseUpView();
            }
        }
    }
}
