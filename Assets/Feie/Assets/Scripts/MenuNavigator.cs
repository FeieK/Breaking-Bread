using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject pausedButtons;
    [SerializeField] private GameObject quitConfirm;
    public int openMenu;

    private GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (gameObject.name == "Menu Navigator")
        {
            if (Input.GetKeyDown("escape"))
            {
                Debug.Log("esc");
                if (gameController.roundIsActive && SceneManager.GetActiveScene().buildIndex == 2)
                {
                    Debug.Log("Scene");
                    if (settingsMenu.activeSelf)
                    {
                        CloseSettings();
                    }
                    else if (!gameController.paused)
                    {
                        gameController.paused = true;
                        PauseGame();
                    }
                    else if (gameController.paused)
                    {
                        gameController.paused = false;
                        PauseGame();
                    }

                }
                else if (gameController.sceneIndex == 1)
                {
                    if (openMenu == 0)
                    {
                        QuitConfirm();
                    }
                    else if (openMenu == 2)
                    {
                        QuitNo();
                    }

                    if (openMenu == 1)
                    {
                        CloseSettings();
                    }
                }
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        if (gameController.roundIsActive)
        {
            pausedButtons.SetActive(false);
        }
        else
        {
            menuScreen.SetActive(false);
            openMenu = 1;
        }
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        if (gameController.roundIsActive)
        {
            pausedButtons.SetActive(true);
        }
        else
        {
            menuScreen.SetActive(true);
            openMenu = 0;
        }
        settingsMenu.SetActive(false);
    }

    public void PauseGame()
    {
        pausedMenu.SetActive(gameController.paused);
    }
    public void StopGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Resume()
    {
        gameController.paused = false;
        pausedMenu.SetActive(gameController.paused);
    }

    public void QuitConfirm()
    {
        quitConfirm.SetActive(true);
        openMenu = 2;
    }
    public void QuitYes()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void QuitNo()
    {
        quitConfirm.SetActive(false);
        openMenu = 0;
    }
}
