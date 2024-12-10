using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tags))]
public class Coleccionable : MonoBehaviour
{
    public Tags itemPrefab; // Prefab del objeto que se recoger�
    public AudioClip recogerSonido; // Sonido al recoger el �tem
    private AudioManager audioManager; // Referencia al AudioManager

    private void Start()
    {
        // Buscar el AudioManager en la escena
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager no encontrado en la escena.");
        }
    }

    public void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform))
        {
            PlayRecogerSonido(); // Reproducir sonido antes de recoger
            CollectItem(); // Recoger el �tem
        }
    }

    private void PlayRecogerSonido()
    {
        // Reproducir el sonido del cable
        if (audioManager != null && recogerSonido != null)
        {
            audioManager.PlaySFX(recogerSonido);
        }
        else
        {
            Debug.LogWarning("No se asign� un sonido para recoger el �tem o el AudioManager no fue encontrado.");
        }
    }

    public void CollectItem()
    {
        // Obtener la instancia de Inventory
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            if (inventory.CollectItem(itemPrefab, GetComponent<Tags>()))
            {
                // Destruir el objeto de la escena
                MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
                if (multipleViewItem != null)
                {
                    multipleViewItem.HideObjectsInAllViews();
                }
                else
                {
                    gameObject.SetActive(false); // Desactivar despu�s de recoger
                }
            }
            else
            {
                Debug.Log("Inventario est� lleno");
            }
        }
        else
        {
            Debug.LogError("InventoryOrder not found!");
        }
    }
}

