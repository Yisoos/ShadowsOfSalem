using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinuarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        SoundManager.Instance.PauseGameAudio(true);
        isPaused = true;
    }

    public void ContinuarJuego()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SoundManager.Instance.PauseGameAudio(false);
        isPaused = false;

    }
    public void EmpezarJuego()
    {
        Debug.Log("Iniciar Juego");
        SceneManager.LoadScene("Intro");

    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menú");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
