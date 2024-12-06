using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines; // L�neas de di�logo del NPC
    public Text dialogueText; // Texto UI para mostrar el di�logo
    public float textSpeed = 0.05f; // Velocidad a la que se muestra cada car�cter
    public float linePause = 2f; // Pausa entre l�neas de di�logo

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
