using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StateManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void exitMain1()
    {
        SceneManager.LoadScene("LevelSelect1");
    }

    public void exitMain2()
    {
        SceneManager.LoadScene("LevelSelect2");
    }
}
