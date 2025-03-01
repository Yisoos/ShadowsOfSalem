using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrofilmDocumentNavigation : MonoBehaviour
{
    private Animator animator;
    public bool[] navigated = new bool[2];
    public int secondsToWaitToFinishLevel;

    [HideInInspector] public bool finishedNavigation;
    private void Start()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < navigated.Length; i++)
        {
            navigated[i] = false;
        }
    }
    private void Update()
    {
        CheckNavigationDone();
        if (finishedNavigation)
        {
            StartCoroutine(FinishLevel());
        }
    }

    public void CheckNavigationDone()
    {
        for (int i = 0; i < navigated.Length; i++)
        {
            if (!navigated[i])
            {
                finishedNavigation = false;
                return;
            }
        }
        finishedNavigation = true;
    }
    public void TurnRight()
    {
        animator.SetTrigger("Right");
        navigated[0] = true;
    }
    public void TurnLeft()
    {
        animator.SetTrigger("Left");
        navigated[1] = true;
    }
    public IEnumerator FinishLevel() 
    {
        yield return new WaitForSeconds(secondsToWaitToFinishLevel); 
        WinLevel winLevel = FindAnyObjectByType<WinLevel>();
        if (winLevel != null)
        {
            winLevel.TryToWinLevel();
        }   
    }

}
