using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameEvents;

    private int classicAccumulatedHits;

    private int classicAccumulatedShots;

    [SerializeField]
    private TextMeshProUGUI classicScoreText;

    [SerializeField]
    private TextMeshProUGUI classicAccumulatedText;

    [SerializeField]
    private TextMeshProUGUI classicAccuracyText;

    private int longrangeAccumulatedHits;

    private int longrangeAccumulatedShots;

    // [SerializeField]
    // private TextMeshProUGUI longrangeScoreText;
    // [SerializeField]
    // private TextMeshProUGUI longrangeAccumulatedText;
    // [SerializeField]
    // private TextMeshProUGUI longrangeAccuracyText;
    private int marathonHighScore;

    private int marathonAccumulatedHits;

    private int marathonAccumulatedShots;

    [SerializeField]
    private TextMeshProUGUI marathonScoreText;

    [SerializeField]
    private TextMeshProUGUI marathonHighScoreText;

    [SerializeField]
    private TextMeshProUGUI marathonAccuracyText;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.OnGameOver += HandleGameOver;
        classicAccumulatedHits =
            PlayerPrefs.GetInt("ClassicAccumulatedHits", 0);
        classicAccumulatedShots =
            PlayerPrefs.GetInt("ClassicAccumulatedShots", 0);
        longrangeAccumulatedHits =
            PlayerPrefs.GetInt("LongrangeAccumulatedHits", 0);
        longrangeAccumulatedShots =
            PlayerPrefs.GetInt("LongrangeAccumulatedShots", 0);
        marathonHighScore = PlayerPrefs.GetInt("MarathonHighScore", 0);
        marathonAccumulatedHits =
            PlayerPrefs.GetInt("MarathonAccumulatedHits", 0);
        marathonAccumulatedShots =
            PlayerPrefs.GetInt("MarathonAccumulatedShots", 0);
    }

    private void HandleGameOver(GameManager.Mode mode)
    {
        switch (mode)
        {
            case GameManager.Mode.Classic:
                SetClassicScores();
                break;
            case GameManager.Mode.Longrange:
                SetLongrangeScores();
                break;
            case GameManager.Mode.Marathon:
                SetMarathonScores();
                break;
            case GameManager.Mode.Multiplayer:
                SetMultiplayerScores();
                break;
            default:
                SetClassicScores();
                break;
        }
    }

    private void SetClassicScores()
    {
        classicScoreText.text = GameManager.score.ToString() + " / 20";
        int newAccumulatedHits = classicAccumulatedHits + GameManager.score;
        int newAccumulatedShots = classicAccumulatedShots + 20;
        classicAccumulatedText.text = newAccumulatedHits.ToString();
        classicAccuracyText.text =
            (
            Math
                .Round((float)(newAccumulatedHits) /
                (newAccumulatedShots) *
                100,
                1)
            ).ToString() +
            " %";
        PlayerPrefs.SetInt("ClassicAccumulatedHits", newAccumulatedHits);
        PlayerPrefs.SetInt("ClassicAccumulatedShots", newAccumulatedShots);
        PlayerPrefs.Save();
    }

    private void SetLongrangeScores()
    {
        classicScoreText.text = GameManager.score.ToString() + " / 20";
        int newAccumulatedHits = classicAccumulatedHits + GameManager.score;
        int newAccumulatedShots = classicAccumulatedShots + 20;
        classicAccumulatedText.text = newAccumulatedHits.ToString();
        classicAccuracyText.text =
            (
            Math
                .Round((float)(newAccumulatedHits) /
                (newAccumulatedShots) *
                100,
                1)
            ).ToString() +
            " %";
        PlayerPrefs.SetInt("LongrangeAccumulatedHits", newAccumulatedHits);
        PlayerPrefs.SetInt("LongrangeAccumulatedShots", newAccumulatedShots);
        PlayerPrefs.Save();
    }

    private void SetMarathonScores()
    {
        marathonScoreText.text = GameManager.score.ToString();
        int newHighScore = Math.Max(marathonHighScore, GameManager.score);
        int newAccumulatedHits = marathonAccumulatedHits + GameManager.score;
        int newAccumulatedShots =
            marathonAccumulatedShots + GameManager.score + 1;

        marathonHighScoreText.text = newHighScore.ToString();

        // marathonAccumulatedText.text = newAccumulatedHits.ToString();
        marathonAccuracyText.text =
            (
            Math
                .Round((float)(newAccumulatedHits) /
                (newAccumulatedShots) *
                100,
                1)
            ).ToString() +
            " %";
        PlayerPrefs.SetInt("MarathonHighScore", newHighScore);
        PlayerPrefs.SetInt("MarathonAccumulatedHits", newAccumulatedHits);
        PlayerPrefs.SetInt("MarathonAccumulatedShots", newAccumulatedShots);
        PlayerPrefs.Save();
    }

    private void SetMultiplayerScores()
    {
        classicScoreText.text = GameManager.score.ToString() + " / 20";
        classicAccumulatedText.text =
            (GameManager.score + classicAccumulatedHits).ToString();
    }
}
