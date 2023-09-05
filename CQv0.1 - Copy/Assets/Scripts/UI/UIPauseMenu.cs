using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public UnityEvent OnPause, OnResume;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] ScenesManager scenesManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        OnResume?.Invoke();
    }
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        OnPause?.Invoke();
    }
    public void LoadMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        scenesManager.LoadMainMenu();
    }
    public void Restart()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        scenesManager.LoadNewGame();
    }
}
