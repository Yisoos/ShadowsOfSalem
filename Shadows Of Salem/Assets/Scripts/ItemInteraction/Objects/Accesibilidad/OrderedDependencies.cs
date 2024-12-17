using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class OrderedDependencies : MonoBehaviour
{
    [Header("Elementos Requeridos")]
    public List<string> requiredItems; // Lista de elementos necesarios para este objeto (por ejemplo, teléfono, cable, pinzas)
    [Space(10), Header("Cambios de Sprite")]
    public Sprite[] dependencyMetSprite; // Sprite cuando el cable está conectado
    [Space(10), Header("FeedbackText")]
    public string[] displayText;
    
    private FeedbackTextController feedbackText;
    private SpriteRenderer spriteRenderer; // Referencia para cambiar el sprite
    private NewInventory inventory; // Referencia al inventario
    [HideInInspector] public bool[] dependencyMet;

    private void Start()
    {
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        inventory = FindAnyObjectByType<NewInventory>();
        // Obtener el componente SpriteRenderer al iniciar
        spriteRenderer = GetComponent<SpriteRenderer>();
        dependencyMet = new bool[requiredItems.Count];
        dependencyMet = Enumerable.Repeat(false, dependencyMet.Length).ToArray();
    }

    // Método para manejar el objeto que se ha soltado y verificar si los elementos requeridos están presentes en el inventario
    public bool HandleItem(InventoryItem objectDropped)
    {
        for(int i = 0;i < dependencyMet.Length; i++)
        {
            if (!dependencyMet[i])
            {
                // Verificar si el inventario está asignado
                if (inventory == null)
                {
                    Debug.LogError("Este script no está conectado al inventario");
                    return false;
                }

                // Iterar a través de los elementos requeridos
                
                    // Verificar si el elemento requerido está presente en el inventario
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

                if (i == requiredItems.Count - 1)
                {
                    feedbackText.PopUpText(displayText[displayText.Length - 1]);
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
            feedbackText.PopUpText(displayText[0]);
            return false;
        }
        for (int i = 0; i < dependencyMet.Length; i++)
        {
            if (dependencyMet[i] && !dependencyMet[dependencyMet.Length-1])
            {
                feedbackText.PopUpText(displayText[i+1]);
                return false;
            }
        }
        return true;
    }
}
