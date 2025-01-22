using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnLoadMainMapButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnTutorialButton()
    {
        SceneManager.LoadScene(3);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton()
    {
        Debug.Log("Oh my God, they killed the program! You bastards!");
        Application.Quit();
    }
}
