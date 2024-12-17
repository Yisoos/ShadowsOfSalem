using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotaryDialControl : MonoBehaviour
{
    [Space(10)] public FeedbackTextController feedbackText;
    [Space(10), Tooltip("Separa cada número con un '-' ")] public string numberToCall;
    [Space(10), Range(0, 15)] public float dialReturnSpeed;
    [Space(10)] public GameObject dialDisplayPrefab;
    [Space(10)] public Transform dialDispalyParent;
    [Space(10)] public Transform UIInventoryDisplay;
    [Space(10)] public TMP_FontAsset[] fontAsset;

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
            Tags prefabPopUpTags = dialDisplayPrefab.GetComponent<Tags>();
            Tags[] allTagsInScene = GameObject.FindObjectsOfType<Tags>(true);
            bool foundMatchingTag = false;

            foreach (Tags tag in allTagsInScene)
            {
                if (tag.objectName == prefabPopUpTags.objectName)
                {
                    tag.gameObject.SetActive(true);
                    tag.transform.SetAsLastSibling();
                    foundMatchingTag = true;
                    break;
                }
            }

            if (!foundMatchingTag)
            {
                GameObject popUp = Instantiate(dialDisplayPrefab, dialDispalyParent);
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
}
