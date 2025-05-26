using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public CanvasGroup startMenu;
    public CanvasGroup settingsMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ClickStart()
    {
    }
    public void ClickSettings()
    {
    }
    public void ClickQuit()
    {
        Debug.Log("Application.Quit");
        // Application.Quit();
    }
}
