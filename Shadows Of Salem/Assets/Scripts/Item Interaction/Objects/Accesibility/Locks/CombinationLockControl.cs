using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationLockControl : MonoBehaviour
{
    public bool isLocked; // Indica si el candado está cerrado
    public string combination; // La combinación para desbloquear este candado
    public GameObject popUpLockPrefab;
    public string[] displayText;

    [Header("Sonidos")]
    public AudioClip clickSound;    // Sonido al interactuar con el candado
    public AudioClip openChestSound; // Sonido al abrir el baúl

    [HideInInspector] public Transform popUpLockParent;
    [HideInInspector] public FeedbackTextController feedbackText;

    private void Start()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        popUpLockParent = FindObjectOfType<Canvas>().transform;
    }

    public void OnMouseDown()
    {
        // Si el candado está bloqueado, mostrar el pop-up
        if (isLocked)
        {
            // Reproducir sonido de clic al interactuar
            AudioManager.Instance.PlaySFX(clickSound);
            StartCoroutine(PopUpWindowManager());
        }
        else
        {
            // Si está desbloqueado, abrir el baúl y reproducir el sonido
            OpenChest();
        }
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f); // Retardo opcional

        if (isLocked && popUpLockPrefab != null)
        {
            CombinationLockPopUp[] allComLocksInScene = GameObject.FindObjectsOfType<CombinationLockPopUp>(true);
            bool foundMatchingTag = false;

            foreach (CombinationLockPopUp combLock in allComLocksInScene)
            {
                if (combLock.combinationLock == this)
                {
                    combLock.gameObject.SetActive(true);
                    foundMatchingTag = true;
                    break;
                }
            }

            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(popUpLockPrefab, popUpLockParent);
                popUp.transform.SetAsLastSibling();
                CombinationLockPopUp popUpScript = popUp.GetComponent<CombinationLockPopUp>();
                popUpScript.combinationLock = this;
            }

            // Desactivar el Collider
            Collider2D objectCollider = GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
            }
        }
    }

    private void OpenChest()
    {
        // Aquí puedes incluir lógica adicional, como animaciones del baúl abriéndose

        // Reproducir sonido de apertura del baúl
        if (openChestSound != null)
        {
            AudioManager.Instance.PlaySFX(openChestSound);
        }

        // Debug opcional para verificar que se abre correctamente
        Debug.Log("El baúl se ha abierto.");
    }
}
