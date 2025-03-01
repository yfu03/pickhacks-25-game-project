using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanager;

    [SerializeField] private TextMeshProUGUI strokeText;
    [Space(10)]
    [SerializeField] private GameObject holeCompleteUI;
    [SerializeField] private TextMeshProUGUI strokeCompletedText;
    [Space(10)]
    [SerializeField] private int par;

    private int strokes;

    public bool outOfStrokes;
    public bool holeCompleted;

    private void Awake()
    {
        gamemanager = this;
    }

    private void Start()
    {
        UpdateStrokeText();
    }

    private void UpdateStrokeText()
    {
        strokeText.text = strokes + "";
    }

    public void increaseStrokes()
    {
        strokes++;
        UpdateStrokeText();
    }

    public void completeHole()
    {
        holeCompleted = true;
        strokeCompletedText.text = strokes > 1 ? "You putted in " + strokes + " strokes." : "Hole in one!";

        holeCompleteUI.SetActive(true);
    }
}
