using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadHowTo()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




}
