using UnityEngine;

public class CajonController : MonoBehaviour
{
    public AudioClip abrirSonido; // Sonido para abrir el caj�n
    public AudioClip cerrarSonido; // Sonido para cerrar el caj�n
    private bool cajonAbierto = false; // Estado del caj�n
    private AudioManager audioManager;

    private void Start()
    {
        // Encuentra el AudioManager en la escena
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager no encontrado en la escena.");
        }
    }

    private void OnMouseDown()
    {
        // Este m�todo se llama autom�ticamente cuando haces clic en el objeto con el script
        if (!cajonAbierto)
        {
            // Abrir el caj�n
            Debug.Log("Sonido abierto");
            cajonAbierto = true;
            if (audioManager != null && abrirSonido != null)
            {
                audioManager.PlaySFX(abrirSonido);
            }
        }
        else
        {
            // Cerrar el caj�n
            Debug.Log("sonido cerrado");
            cajonAbierto = false;
            if (audioManager != null && cerrarSonido != null)
            {
                audioManager.PlaySFX(cerrarSonido);
            }
        }
    }
}
