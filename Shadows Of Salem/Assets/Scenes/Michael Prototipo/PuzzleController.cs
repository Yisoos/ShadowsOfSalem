using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PuzzleController : MonoBehaviour
{
    public StatuePuzzle statuePuzzle;
    public MapFragmentPuzzle mapFragmentPuzzle;

    void Update()
    {
       // if (statuePuzzle.IsPuzzleSolved() && mapFragmentPuzzle.IsPuzzleSolved())
        {
            StartCoroutine(FinalPuzzleSolved());
        }
    }

    IEnumerator FinalPuzzleSolved()
    {
        Debug.Log("¡Todos los acertijos han sido resueltos!");
        yield return null;
    }
}
