using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startButton()
    {
        SceneManager.LoadScene("Level01"); 
    }

    public void levelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect1"); 
    }


    public void quitButton()
    {
        Application.Quit();
    }
}
