using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject configPanel;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        configPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        configPanel.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        configPanel.SetActive(false);
    }

    public void ShowConfig()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        configPanel.SetActive(true);
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        UnlockLevels.unlockLevel2 = false;
        Application.Quit();
    }
}
