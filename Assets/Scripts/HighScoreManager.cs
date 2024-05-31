using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    [SerializeField] private int savedHighScore;
    [SerializeField] private int currentHighScore;
    private string highScore = "HighScore";


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentHighScore = PlayerPrefs.GetInt(highScore, 0);
    }

    private void Start()
    {
        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public int GetHighScore()
    {
        return currentHighScore;
    }

    public void SaveHighScore(int score)
    {
        currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
