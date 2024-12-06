using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines; // Líneas de diálogo del NPC
    public Text dialogueText; // Texto UI para mostrar el diálogo
    public float textSpeed = 0.05f; // Velocidad a la que se muestra cada carácter
    public float linePause = 2f; // Pausa entre líneas de diálogo

    private bool isTalking = false;

    void Update()
    {
        // Detectar si el jugador hace clic en el NPC
        if (Input.GetMouseButtonDown(0) && !isTalking)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    StartCoroutine(StartDialogue());
                }
            }
        }
    }

    IEnumerator StartDialogue()
    {
        isTalking = true;

        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(DisplayLine(line));
            yield return new WaitForSeconds(linePause);
        }

        dialogueText.text = "";
        isTalking = false;
    }

    IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
