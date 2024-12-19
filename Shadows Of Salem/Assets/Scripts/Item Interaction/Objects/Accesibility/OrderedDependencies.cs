using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class OrderedDependencies : MonoBehaviour
{
    [Header("Elementos Requeridos")]
    public List<string> requiredItems; // Lista de elementos necesarios que se deben cumplir en orden.

    [Space(10), Header("Cambios de Sprite")]
    public Sprite[] dependencyMetSprite; // Array de sprites para cambiar la apariencia al cumplir cada dependencia.

    [Space(10), Header("FeedbackText")]
    public string itemRejectionText;
    public string[] displayText; // Mensajes de texto que se mostrarán al cumplir o no las dependencias.

    private FeedbackTextController feedbackText; // Referencia al controlador de mensajes emergentes.
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer para cambiar sprites.
    private Inventory inventory; // Referencia al inventario para manejar objetos.
    [HideInInspector] public bool[] dependencyMet; // Array booleano para registrar el estado de cada dependencia.

    private void Start()
    {
        // Inicialización de referencias necesarias.
        feedbackText = FindAnyObjectByType<FeedbackTextController>();
        inventory = FindAnyObjectByType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializa el array de estados de dependencias como "false".
        dependencyMet = Enumerable.Repeat(false, requiredItems.Count).ToArray();
    }

    public bool HandleItem(InventoryItem objectDropped)
    {
        for (int i = 0; i < dependencyMet.Length; i++)
        {
            // Si la dependencia aún no se ha cumplido:
            if (!dependencyMet[i])
            {
                // Verifica si el inventario está configurado.
                if (inventory == null)
                {
                    Debug.LogError("Este script no está conectado al inventario");
                    return false;
                }

                // Comprueba si el objeto soltado coincide con el requerido en esta posición.
                bool itemFound = requiredItems[i] == objectDropped.tagInfo.objectName;

                if (!itemFound)
                {
                    if (feedbackText != null) 
                    {
                        // Si el objeto no es el requerido, muestra un mensaje de error.
                        if (!requiredItems.Contains(objectDropped.tagInfo.objectName))
                        {
                            feedbackText.PopUpText(itemRejectionText);
                        }
                        else if (objectDropped.tagInfo.displayText.Length > 0)
                        {
                            feedbackText.PopUpText(objectDropped.tagInfo.displayText[0]);
                        }
                    }
                    return false; // Termina la función sin procesar el objeto.
                }

                // Si se cumple la última dependencia, muestra un mensaje final.
                if (feedbackText != null && objectDropped.tagInfo.displayText.Length > 0)
                {
                    feedbackText.PopUpText(objectDropped.tagInfo.displayText[^1]);
                }
                spriteRenderer.sprite = dependencyMetSprite[i + 1]; // Actualiza el sprite al correspondiente al progreso actual.
                dependencyMet[i] = true; // Marca la dependencia actual como cumplida.
                inventory.DeleteItem(objectDropped,1);// Elimina el objeto del inventario.
                break; // Termina el bucle después de procesar el objeto.
            }
        }
        return true; // Retorna true si el proceso se completó correctamente.
    }

    public bool FeedbackTextDesider()
    {
        // Si la primera dependencia no se ha cumplido, muestra un mensaje inicial.
        if (!dependencyMet[0])
        {
            if(feedbackText != null && displayText.Length > 0)
            {
                feedbackText.PopUpText(displayText[0]);
            }
            return false;
        }

        // Recorre las dependencias para mostrar mensajes de progreso.
        for (int i = 0; i < dependencyMet.Length; i++)
        {
            if (dependencyMet[i] && !dependencyMet[^1])
            {
                if (feedbackText != null && displayText.Length > 0)
                {
                    feedbackText.PopUpText(displayText[i + 1]);
                }
                return false; // Termina si aún hay dependencias sin cumplir.
            }
        }
        return true; // Todas las dependencias están cumplidas.
    }
}
