using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager gamemanager;

    [SerializeField] private TextMeshProUGUI strokeText;
    [SerializeField] private TextMeshProUGUI parText;
    [SerializeField] private AudioSource audsrc;
    [Space(10)]
    [SerializeField] private GameObject holeCompleteUI;
    [SerializeField] private TextMeshProUGUI strokeCompletedText;
    [SerializeField] private TextMeshProUGUI bombText;
    [Space(10)]
    [SerializeField] private int par;

    public AudioClip bgm, ambience, putt, hole, explosion, boing;

    private int strokes;
    private int numBomb = 1;
    private string sceneName;

    Dictionary<string, int> levelPars = new Dictionary<string, int>()
    { //ALL OF THESE PARS ARE SUBJECT TO CHANGE!
        { "Level01", 2 },
        { "Level02", 3 },
        { "Level03", 5 },
        { "Level21", 2 },
        { "Level22", 5 },
        { "Level23", 5 },
        { "Level31", 5 },
        { "Level32", 5 },
        { "Level33", 5 },
    };

    public bool outOfStrokes;
    public bool holeCompleted;

    private void Awake()
    {
        gamemanager = this;
    }

    private void Start()
    {
        UpdateStrokeText();
        setPar();
    }

    private void Update()
    {
        restartLevel();
    }

    private void UpdateStrokeText()
    {
        strokeText.text = "Stroke " + strokes;
    }

    public void UpdateBombText()
    {
        bombText.text = "X " + numBomb;
    }

    private void UpdateParText()
    {
        parText.text = "Par " + par;
    }

    public void increaseStrokes()
    {
        strokes++;
        UpdateStrokeText();
    }

    public void useBomb()
    {
        numBomb--;
        UpdateBombText();
    }

    public void completeHole()
    {
        holeCompleted = true;
        strokeCompletedText.text = strokes > 1 ? "You putted in " + strokes + " strokes." : "Hole in one!";

        holeCompleteUI.SetActive(true);
    }

    private void setPar()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        par = levelPars[sceneName];
        //UnityEngine.Debug.Log("Par: " + par);
        UpdateParText();
    }

    private void restartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && !holeCompleted)
        {
            UnityEngine.Debug.Log("restarting level");
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
