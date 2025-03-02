using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect2 : MonoBehaviour
{
    public void W11Button()
    {
        SceneManager.LoadScene("Level21");
    }

    public void W12Button()
    {
        SceneManager.LoadScene("Level22");
    }


    public void W13Button()
    {
        SceneManager.LoadScene("Level23");
    }

    public void backButton()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void nextButton()
    {
        SceneManager.LoadScene("LevelSelect1");
    }
}
