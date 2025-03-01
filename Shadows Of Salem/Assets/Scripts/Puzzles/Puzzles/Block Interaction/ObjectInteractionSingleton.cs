using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionSingleton : MonoBehaviour
{
    public static ObjectInteractionSingleton Instance;
    public bool isColliderEnabled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }
}
