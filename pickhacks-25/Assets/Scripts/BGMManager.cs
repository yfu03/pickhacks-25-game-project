using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audsrc;
    public static BGMManager bgmmanager;

    public AudioClip bgm;

    private void Awake()
    {
        bgmmanager = this;
    }

    private void Update()
    {
        PlayMusic();
        StopMusic();
    }

    public void PlayMusic()
    {
        if (audsrc.isPlaying) return;
        string sceneName = getSceneName();
        if (sceneName != "TitleScreen" || sceneName != "LevelSelect1" || sceneName != "LevelSelect2")
            audsrc.Play();
    }

    public void StopMusic()
    {
        string sceneName = getSceneName();
        if (sceneName == "TitleScreen" || sceneName == "LevelSelect1" || sceneName == "LevelSelect2")
            audsrc.Stop();
    }

    public string getSceneName()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }
}
