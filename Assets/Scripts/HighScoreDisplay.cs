using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private int highScore;

    void Start()
    {
        if (highScoreText == null)
        {
            Debug.LogError("HighScoreText no assignned");
            return;
        }

        UpdateHighScoreText();
    }

    public void UpdateHighScoreText()
    {
        highScore = HighScoreManager.Instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
