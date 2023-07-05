using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenLevels()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuExitGame()
    {
        Application.Quit();
    }
}
