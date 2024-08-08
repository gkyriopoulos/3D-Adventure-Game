using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void MenuPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void MenuQuit()
    {
        Application.Quit();
    }

    public void LoadCredits(){
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
