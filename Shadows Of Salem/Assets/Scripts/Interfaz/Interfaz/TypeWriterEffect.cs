using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour
{
    AudioManager audioManager; // Para controlar el sonido
    TMP_Text _tmpProText;
    string originalText; // Almacena el texto original del componente TMP antes de que se inicie el efecto

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    [SerializeField] GameObject flechita;
    private bool isTyping = false;
    private bool hasFinishedTyping = false;

    [TextArea(1, 10), SerializeField] string[] phrases; // Array de frases
    private int currentPhraseIndex = 0; // Para rastrear qué frase está siendo escrita

    public float titleDisplayDelay = 1.7f; // Retraso antes de activar el título del nivel (texto)
    [SerializeField] TMP_Text title;

    private bool clicActivado = true;
    public CambiarEscenas changeScenes;

    [SerializeField] GameObject imagenFinal;

    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Se utiliza para reproducir los efectos de sonido
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
        }
    }

    void Start()
    {
        _tmpProText = GetComponent<TMP_Text>();
        if (title != null)
        {
            title.enabled = false;
        }

        if (imagenFinal !=  null) 
        {
            imagenFinal.SetActive(false);
        }


        if (_tmpProText != null)
        {
            originalText = _tmpProText.text; // Guarda el texto original en "writer"
            _tmpProText.text = ""; // Limpia el texto del componente TMP_Text

            if (flechita != null)
            {
                flechita.SetActive(false);
            }

            changeScenes = FindObjectOfType<CambiarEscenas>();
            if (phrases == null || phrases.Length == 0)
            {
                Debug.LogError("Error: No phrases assigned!");
                return;
            }
            StartCoroutine(TypePhrase(phrases[currentPhraseIndex])); // Inicia la corrutina para realizar el efecto de máquina de escribir
        }
    }

    IEnumerator TypePhrase(string phrase)
    {
        // Comprobar si estamos en la escena de nivel 0
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene); // Verifica el nombre de la escena actual en la consola

        // Solo reproducir el sonido si no estamos en la escena 'Nivel0'
        if (currentScene != "Nivel0")
        {
            Debug.Log("Reproduciendo sonido de escritura"); // Depuración para asegurar que el sonido se reproduce solo si no estamos en Nivel0
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.audioData.saltoDeEscritura);
            }
        }
        else
        {
            Debug.Log("No se reproduce sonido en Nivel0"); // Confirmar que no se reproduce sonido en Nivel0
        }

        isTyping = true;
        hasFinishedTyping = false;
        originalText = phrase;
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : ""; // Mostrar o no el leadingChar

        yield return new WaitForSeconds(delayBeforeStart); // Espera el tiempo especificado antes de comenzar a escribir

        foreach (char letter in phrase.ToCharArray())
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length); // Si hay algún texto en pantalla, primero quita el leadingChar temporalmente
            }
            _tmpProText.text += letter; // Añade el siguiente carácter
            _tmpProText.text += leadingChar; // Vuelve a añadir el leadingChar para dar la ilusión de que el cursor sigue parpadeando o moviéndose después de cada carácter
            yield return new WaitForSeconds(timeBtwChars);
        }
        _tmpProText.text = phrase; // Asegura que la frase completa se muestre al final
        isTyping = false; // El efecto de escribir ha terminado
        hasFinishedTyping = true; // La frase ha terminado de escribirse
        AudioListener.pause = true;

        if (flechita != null)
        {
            flechita.SetActive(true); // Activa el GameObject
        }
    }

    void Update()
    {
        if (!clicActivado)
        {
            return;
        }

        // Comprobar si el jugador intenta avanzar mientras el texto aún está siendo escrito
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                _tmpProText.text = originalText; // El texto actual igual al original 
                isTyping = false; // Efecto terminado
                hasFinishedTyping = true;
                AudioListener.pause = true;

                // Activar flechita ya que se ha saltado el efecto typewriter
                if (flechita != null)
                {
                    flechita.SetActive(true);
                }
            }
            return;
        }

        // Si ha terminado de escribir la frase actual y detecta un clic, empezar escribiendo la siguiente
        if (hasFinishedTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ShowNextPhrase();
        }
    }

    void ShowNextPhrase()
    {
        currentPhraseIndex++; // Siguiente frase

        if (currentPhraseIndex < phrases.Length)
        {
            _tmpProText.text = ""; // Borramos todo el texto que teníamos
            flechita.SetActive(false); // Desactivar flechita
            AudioListener.pause = false;

            StartCoroutine(TypePhrase(phrases[currentPhraseIndex])); // Empezamos a escribir la siguiente frase
        }
        else
        {
            Debug.Log("All phrases completed.");
            flechita.SetActive(false);
            _tmpProText.enabled = false;
            AudioListener.pause = true;
            StartCoroutine(EnableTitleDisplayDelay());
        }
    }

    IEnumerator EnableTitleDisplayDelay()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Wait");
        if (title != null)
        {
            title.enabled = true;
        }
        if (imagenFinal != null)
        {
            imagenFinal.SetActive(true);
        }
        clicActivado = false;

        yield return new WaitForSeconds(titleDisplayDelay);
        Debug.Log("Display title");
        if (title != null)
        {
            title.enabled = false;
        }

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Changing scenes");

        // Cambiar de escena (nivel 0) cuando termine de escribir todas las frases
        if (changeScenes != null)
        {
            clicActivado = true;
            changeScenes.CambiarEscenaSinInput(); // Método del script "CambiarEscenas"
            AudioListener.pause = false;
        }
    }
}
