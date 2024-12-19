using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotaryDialControl : MonoBehaviour
{
<<<<<<< HEAD:Shadows Of Salem/Assets/Scripts/ItemInteraction/ItemInteraction/Objects/Phone/RotaryDialControl.cs
    [Space(10)] public FeedbackTextController feedbackText;
    [Space(10), Tooltip("Separa cada número con un '-' ")] public string numberToCall;
    [Space(10), Range(0, 15)] public float dialReturnSpeed;
    [Space(10)] public GameObject dialDisplayPrefab;
    [Space(10)] public Transform dialDispalyParent;
    [Space(10)] public Transform UIInventoryDisplay;
    [Space(10)] public TMP_FontAsset[] fontAsset;
=======
    [Header("Ajustes del Telefono")]
    [Tooltip("Separa cada número con un '-' ")]public string numberToCall; // The key to unlock this lock
    [Space(5)]public GameObject dialDisplayPrefab;
    [Space(5), Range(0, 15)] public float dialReturnSpeed; // Velocidad de retorno del dial a su posición inicial

    [Space(10), Header("Ajustes de Texto")]
    public string[] displayText;
    [Space(5)] public TMP_FontAsset[] fontAsset;
    
    [Space(10), Header("Otros Ajustes")]
    public Transform UIInventoryDisplay;

    [HideInInspector] public FeedbackTextController feedbackText;
    [HideInInspector] public Transform dialDisplayParent;

    private void Start()
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        dialDisplayParent = FindObjectOfType<Canvas>().transform;
    }
>>>>>>> Inventory-update:Shadows Of Salem/Assets/Scripts/ItemInteraction/Objects/Phone/RotaryDialControl.cs

    [Header("Audio Settings")]
    public AudioSource dialAudioSource; // Referencia al AudioSource
    public AudioClip dialSoundClip;     // Clip de sonido para el giro de la rueda

    public void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.transform))
        {
            StartCoroutine(PopUpWindowManager());
        }
    }

    private IEnumerator PopUpWindowManager()
    {
        yield return new WaitForSeconds(0.2f);

        if (dialDisplayPrefab != null)
        {
<<<<<<< HEAD:Shadows Of Salem/Assets/Scripts/ItemInteraction/ItemInteraction/Objects/Phone/RotaryDialControl.cs
            Tags prefabPopUpTags = dialDisplayPrefab.GetComponent<Tags>();
            Tags[] allTagsInScene = GameObject.FindObjectsOfType<Tags>(true);
            bool foundMatchingTag = false;
=======
            RotaryDial[] allDialsInScene = GameObject.FindObjectsOfType<RotaryDial>(true);
            bool foundMatchingTag = false; // Track if a matching tag is found
>>>>>>> Inventory-update:Shadows Of Salem/Assets/Scripts/ItemInteraction/Objects/Phone/RotaryDialControl.cs

            foreach (RotaryDial dial in allDialsInScene)
            {
                if (dial.phoneParent == this)
                {
<<<<<<< HEAD:Shadows Of Salem/Assets/Scripts/ItemInteraction/ItemInteraction/Objects/Phone/RotaryDialControl.cs
                    tag.gameObject.SetActive(true);
                    tag.transform.SetAsLastSibling();
                    foundMatchingTag = true;
                    break;
=======
                    dial.gameObject.SetActive(true);
                    dial.transform.SetAsLastSibling();
                    foundMatchingTag = true; // Mark that we found a match
                    break; // Exit loop once a match is found
>>>>>>> Inventory-update:Shadows Of Salem/Assets/Scripts/ItemInteraction/Objects/Phone/RotaryDialControl.cs
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

            Collider2D objectCollider = GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
            }
        }
    }
<<<<<<< HEAD:Shadows Of Salem/Assets/Scripts/ItemInteraction/ItemInteraction/Objects/Phone/RotaryDialControl.cs

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
        dialDispalyParent = FindObjectOfType<Canvas>().transform;

        // Configura el AudioSource automáticamente
        dialAudioSource = GetComponent<AudioSource>();
    }
=======
>>>>>>> Inventory-update:Shadows Of Salem/Assets/Scripts/ItemInteraction/Objects/Phone/RotaryDialControl.cs
}
