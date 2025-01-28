using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotaryDialControl : MonoBehaviour
{
    [Header("Ajustes del Telefono")]
    [Tooltip("Separa cada número con un '-' ")]public string numberToCall; // The key to unlock this lock
    [Space(5)]public GameObject dialDisplayPrefab;
    [Space(5), Range(0, 15)] public float dialReturnSpeed; // Velocidad de retorno del dial a su posición inicial

    [Space(10), Header("Ajustes de Texto")]
    public string[] displayText;
    [Space(5)] public TMP_FontAsset[] fontAsset;
    
    [Space(10), Header("Audio Settings")]
    public AudioSource dialAudioSource; // Referencia al AudioSource
    public AudioClip dialSoundClip;     // Clip de sonido para el giro de la rueda

    [Space(10), Header("Otros Ajustes")]
    public Transform UIInventoryDisplay;



    [HideInInspector] public FeedbackTextController feedbackText;
    [HideInInspector] public Transform dialDisplayParent;

    /*[HideInInspector]*/ public bool isDialOpened = false;

    private void Start()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        dialDisplayParent = FindObjectOfType<Canvas>().transform;
    }
    public void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.transform) && isDialOpened == false)
        {
            StartCoroutine(PopUpWindowManager());
        }
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f);

        if (dialDisplayPrefab != null)
        {
            RotaryDial[] allDialsInScene = GameObject.FindObjectsOfType<RotaryDial>(true);
            bool foundMatchingTag = false; // Track if a matching tag is found

            foreach (RotaryDial dial in allDialsInScene)
            {
                if (dial.phoneParent == this)
                {
                    dial.popUp.gameObject.SetActive(true);
                    dial.popUp.transform.SetAsLastSibling();
                    foundMatchingTag = true; // Mark that we found a match
                    break; // Exit loop once a match is found
                }
            }

            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(dialDisplayPrefab, dialDisplayParent);
                RotaryDial popUpScript = popUp.GetComponentInChildren<RotaryDial>();
                popUpScript.phoneParent = this;
                popUpScript.phoneNumberDisplay.text = string.Empty;

                // Reproduce el sonido al girar la rueda
                PlayDialSound();
                popUp.transform.SetAsLastSibling();
            }
            
        }
    }

    private void PlayDialSound()
    {
        if (dialAudioSource != null && dialSoundClip != null)
        {
            dialAudioSource.clip = dialSoundClip;
            dialAudioSource.Play();
        }
    }

    [ContextMenu("Conectar componentes generales")]
    private void ConectarComponentesGenerales()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        dialDisplayParent = FindObjectOfType<Canvas>().transform;

        // Configura el AudioSource automáticamente
        dialAudioSource = GetComponent<AudioSource>();
    }
}
