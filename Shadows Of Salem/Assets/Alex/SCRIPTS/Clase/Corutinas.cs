using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Corutinas : MonoBehaviour
{
    public SpriteRenderer circulo;

    // Start is called before the first frame update
    void Start()
    {
        circulo = GetComponent<SpriteRenderer>();
        StartCoroutine(Prueba());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Prueba()
    {
        yield return new WaitForSeconds(1f);
        circulo.color = new Color(255, 0, 189, 255); //rosa
        yield return new WaitForSeconds(1f);
        circulo.color = new Color(0, 0, 255, 255); //azul oscuro
        yield return new WaitForSeconds(1f);
        circulo.color = new Color(0, 249, 255, 255); //teal
        yield return null;



    }
}
