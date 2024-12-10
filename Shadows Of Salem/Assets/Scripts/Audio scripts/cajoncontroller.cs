using UnityEngine;

public class CajonController : MonoBehaviour
{
    public AudioClip abrirSonido; // Sonido para abrir el cajón
    public AudioClip cerrarSonido; // Sonido para cerrar el cajón
    private bool cajonAbierto = false; // Estado del cajón
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
        // Este método se llama automáticamente cuando haces clic en el objeto con el script
        if (!cajonAbierto)
        {
            // Abrir el cajón
            Debug.Log("Sonido abierto");
            cajonAbierto = true;
            if (audioManager != null && abrirSonido != null)
            {
                audioManager.PlaySFX(abrirSonido);
            }
        }
        else
        {
            // Cerrar el cajón
            Debug.Log("sonido cerrado");
            cajonAbierto = false;
            if (audioManager != null && cerrarSonido != null)
            {
                audioManager.PlaySFX(cerrarSonido);
            }
        }
    }
}
