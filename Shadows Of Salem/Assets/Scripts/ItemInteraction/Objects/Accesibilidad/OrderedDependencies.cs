using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class OrderedDependencies : MonoBehaviour
{
    
    public NewTags thisTag;
    [Header("Elementos requeridos")]
    [Space (20)] public List<string> requiredItems; // Lista de elementos necesarios para este objeto (por ejemplo, tel�fono, cable, pinzas)
    [Space(20)] public NewInventory inventory; // Referencia al inventario
    [Space(20)] public Sprite[] dependencyMetSprite; // Sprite cuando el cable est� conectado
    [Space(20)] public FeedbackTextController feedbackText;
    private SpriteRenderer spriteRenderer; // Referencia para cambiar el sprite
    [HideInInspector] public bool[] dependencyMet;

    private void Start()
    {
        // Obtener el componente SpriteRenderer al iniciar
        spriteRenderer = GetComponent<SpriteRenderer>();
        dependencyMet = new bool[requiredItems.Count];
        dependencyMet = Enumerable.Repeat(false, dependencyMet.Length).ToArray();
    }

    // M�todo para manejar el objeto que se ha soltado y verificar si los elementos requeridos est�n presentes en el inventario
    public bool HandleItem(InventoryItem objectDropped)
    {
        for(int i = 0;i < dependencyMet.Length; i++)
        {
            if (!dependencyMet[i])
            {
                // Verificar si el inventario est� asignado
                if (inventory == null)
                {
                    Debug.LogError("Este script no est� conectado al inventario");
                    return false;
                }

                // Iterar a trav�s de los elementos requeridos
                
                    // Verificar si el elemento requerido est� presente en el inventario
                    bool itemFound = requiredItems[i] == objectDropped.tagInfo.objectName;

                    // Si el elemento requerido no se encuentra en el inventario, registrar un mensaje y devolver falso
                    if (!itemFound)
                    {
                        if (feedbackText != null || objectDropped != null && objectDropped.tagInfo.displayText[0] != null)
                        {
                            feedbackText.PopUpText(objectDropped.tagInfo.displayText[0]);
                        }
                        //Debug.Log($"Para usar este objeto necesitas {requiredItems[i]}");
                        return false;
                    }
                

                // Todos los elementos requeridos fueron encontrados
                //Debug.Log("objeto requerido detectado.");

                if (thisTag != null || i == requiredItems.Count - 1)
                {
                    feedbackText.PopUpText(thisTag.displayText[thisTag.displayText.Length - 2]);
                }
                else
                {
                    feedbackText.PopUpText(objectDropped.tagInfo.displayText[1]);
                } 

                spriteRenderer.sprite = dependencyMetSprite[i+1];
                dependencyMet[i] = true;

                    // Eliminar el elemento del inventario
                    inventory.DeleteItem(objectDropped);
            }
        }
        return true;
    }
    public bool feedbackTextDesider() 
    {
        if (!dependencyMet[0])
        {
            feedbackText.PopUpText(thisTag.displayText[0]);
            return false;
        }
        for (int i = 0; i < dependencyMet.Length; i++)
        {
            if (dependencyMet[i] && !dependencyMet[dependencyMet.Length-1])
            {
                feedbackText.PopUpText(thisTag.displayText[i+1]);
                return false;
            }
        }
        return true;
    }
    [ContextMenu("Conectar componentes generales")]
    private void ConectarComponentesGenerales() 
    {
        feedbackText = FindFirstObjectByType<FeedbackTextController>();
        inventory = FindFirstObjectByType<NewInventory>();
    }
}
