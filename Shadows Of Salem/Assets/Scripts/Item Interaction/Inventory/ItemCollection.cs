using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public InventoryItem itemPrefab;
    [Min(0)] public int amountToCollect = 1;
    public Tags inheritTags;

    [Header("Sonido de recolección")]
    public AudioClip collectSound;  // Sonido cuando el objeto es recolectado

    private void Start()
    {
        inheritTags.transform = transform;
    }

    public void OnMouseDown()
    {
        if (AccesibilityChecker.Instance.ObjectAccessibilityChecker(this.gameObject.transform))
        {
            CollectItem();
        }
    }

    public void CollectItem()
    {
        // Obtener la instancia de Inventory
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            if (inventory.CollectItem(itemPrefab, inheritTags, amountToCollect))
            {
                // Reproducir sonido de recolección
                if (collectSound != null)
                {
                    AudioManager.Instance.PlaySFX(collectSound);
                }

                // Destruir el objeto de la escena
                MultipleViewItem multipleViewItem = GetComponent<MultipleViewItem>();
                if (multipleViewItem != null)
                {
                    multipleViewItem.HideObjectsInAllViews();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Inventario está lleno");
            }
        }
        else
        {
            Debug.LogError("Inventory not found!");
        }
    }
}
