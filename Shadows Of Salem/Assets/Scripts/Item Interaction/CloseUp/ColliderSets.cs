using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColliderSets
{
    [Tooltip("Arrastrar el/los objetos como quieres que aparezca al entrar en la vista close up")] public Collider2D estadoInicial;
    [Tooltip("Arrastrar el/los objetos en el estado que quieres que aparezcan al hacer click en él/ellos (si no quieres que cambie, pon el mismo del estado inicial)")] public Collider2D estadoAlternativo;
}
