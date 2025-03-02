using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private AudioSource audsrc;
    public static AmbienceManager ambmanager;

    public AudioClip bgm;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        ambmanager = this;
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
        if (sceneName.Length > 7)
            audsrc.Play();
    }

    public void StopMusic()
    {
        string sceneName = getSceneName();
        //UnityEngine.Debug.Log(sceneName.Length);
        if (sceneName.Length == 7)
            audsrc.Stop();
    }

    public string getSceneName()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }
}
