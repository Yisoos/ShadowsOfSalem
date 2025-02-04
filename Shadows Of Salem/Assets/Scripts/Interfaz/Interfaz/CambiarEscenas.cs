using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenas : MonoBehaviour
{
    public string sceneName;

    public void CambiarEscena()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Llamar al AudioManager para cambiar la m�sica de fondo antes de cargar la siguiente escena
            ChangeBackgroundMusicForNextScene();

            // Cargar la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void CambiarEscenaSinInput()
    {
        // Llamar al AudioManager para cambiar la m�sica de fondo
        AudioManager.Instance.ChangeBackgroundMusic(AudioManager.Instance.audioData.backgroundLv0); // Cambiar al AudioClip de la escena nivel 0

        // Cargar la escena directamente
        SceneManager.LoadScene(sceneName);
        ChangeBackgroundMusicForNextScene();
    }

    public void ChangeToScene(string sceneName)
    {
        // Llamar al AudioManager para cambiar la m�sica de fondo
        AudioManager.Instance.ChangeBackgroundMusic(AudioManager.Instance.audioData.backgroundLv0); // Cambiar al AudioClip de la escena nivel 0

        // Cargar la escena
        SceneManager.LoadScene(sceneName);
        ChangeBackgroundMusicForNextScene();
    }

    private void ChangeBackgroundMusicForNextScene()
    {
        // Obtener el nombre de la siguiente escena
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextSceneName = SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;

        // Cambiar m�sica seg�n la escena siguiente (aqu� tambi�n puedes agregar m�s l�gica si es necesario)
        if (nextSceneName == "Nivel 0") // Aqu� debes especificar los nombres de tus escenas
        {
            AudioManager.Instance.StopPlayingAudio();
            AudioManager.Instance.ChangeBackgroundMusic(AudioManager.Instance.audioData.backgroundLv0);
        }
        else
        {
            AudioManager.Instance.StopPlayingAudio(); // Cambiar a la m�sica de introducci�n si no es "Nivel0"
        }
    }
}
