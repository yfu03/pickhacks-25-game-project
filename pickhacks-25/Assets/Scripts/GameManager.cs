using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RawImage scoreSprite;
    [Space(10)]
    [SerializeField] private int par;

    public AudioClip bgm, ambience, putt, hole, explosion, boing, spring;
    public List<Texture> rankSprites;

    private int strokes;
    private int numBomb = 1;
    private string sceneName;

    Dictionary<string, int> levelPars = new Dictionary<string, int>()
    { //ALL OF THESE PARS ARE SUBJECT TO CHANGE!
        { "Level01", 2 },
        { "Level02", 4 },
        { "Level03", 4 },
        { "Level21", 3 },
        { "Level22", 4 },
        { "Level23", 5 },
        { "Level31", 5 },
        { "Level32", 5 },
        { "Level33", 5 },
    };

    public bool outOfStrokes;
    private bool holeCompleted = false;

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

    public void playPuttSound()
    {
        audsrc.clip = putt;
        audsrc.Play();
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
        strokeCompletedText.text = "You putted in " + strokes + " strokes.";
        scoreText.text = computeScore();
        holeCompleteUI.SetActive(true);
    }

    private string computeScore()
    {

        if (strokes == 1)
        {
            scoreSprite.texture = rankSprites[0];
            return "Hole in one!";
        }
        int score = strokes - par;
        switch (score)
        {
            case -2:
                scoreSprite.texture = rankSprites[0];
                return "Eagle";
            case -1:
                scoreSprite.texture = rankSprites[0];
                return "Birdie";
            case 0:
                scoreSprite.texture = rankSprites[0];
                return "Par";
            case 1:
                scoreSprite.texture = rankSprites[1];
                return "Bogey";
            case 2:
                scoreSprite.texture = rankSprites[2];
                return "Double Bogey";
            case 3:
                scoreSprite.texture = rankSprites[3];
                return "Triple Bogey";
            default:
                scoreSprite.texture = rankSprites[4];
                return score + " Bogey";
        }
    }

    public void playHoleSound()
    {
        audsrc.clip = hole;
        audsrc.Play();
    }

    public void playBombSound()
    {
        audsrc.clip = explosion;
        audsrc.Play();
    }

    public void playBounceSound()
    {
        audsrc.clip = boing;
        audsrc.Play();
    }

    public void playSpringSound()
    {
        UnityEngine.Debug.Log("HI ");
        audsrc.clip = spring;
        audsrc.Play();
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
