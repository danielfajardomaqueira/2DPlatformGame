using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    public TextMeshProUGUI highScoreText;

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

        // Asegurar de que highScoreText est� presente
        GameObject textObject = GameObject.FindGameObjectWithTag("HighScoreText");
        if (textObject != null)
        {
            highScoreText = textObject.GetComponent<TextMeshProUGUI>();
            if (highScoreText == null)
            {

                Debug.LogError("HighScoreText no tiene el componente TextMeshProUGUI.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto con la etiqueta HighScoreText.");
        }
    }

    private void Start()
    {

        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + savedHighScore.ToString();
        }
    }

    public void SaveHighScore(int score)
    {
        currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + score.ToString();
            }
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(highScore); // Borrar el puntaje alto guardado
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: 0"; // Actualizar el texto en la interfaz de usuario
        }
    }
 
}
