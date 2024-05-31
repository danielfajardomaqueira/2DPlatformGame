using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    public static GameManager Instance { get; private set; }
    
    public int TotalPoints { get { return totalPoints; }/*,set*/ } //Write-only property
    public int TotalGems { get { return totalGems; } }              //From another script, return the value of the private variable(in this case, totalGems)
    static public int totalGems;

    public HUD hud;
    public GameObject gameOverUI;
    

    //----PRIVATE VARIABLES----
    private int totalPoints;
    [SerializeField]private int lives = 3;
    
    private CharacterController character;
    private HighScoreManager highScoreManager;


    //----SINGLETON----
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning, More than one GameManager in the scene");
        }
    }
    //-----------------

    void Start()
    {
        character = FindObjectOfType<CharacterController>();
        
        highScoreManager = FindObjectOfType<HighScoreManager>();
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager no encontrado en la escena.");
        }
        else
        {
            // Load high score when starting the game
            int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
            hud.UpdateScore(savedHighScore);
        }

        totalPoints = 0;
        hud.UpdateScore(totalPoints);
    }

    public void ScorePoints(int pointsToAdd)
    {
        totalPoints = totalPoints + pointsToAdd;
        hud.UpdateScore(TotalPoints);
        highScoreManager.SaveHighScore(TotalPoints);
    }

    public void ScoreGems(int gemsToAdd)
    {
        totalGems = totalGems + gemsToAdd;
        hud.UpdateGems(TotalGems);
    }

    public void LoseLive()
    {
        lives -= 1;
        if(lives == 0)
        {
            //Game Over
            CharacterController.isDead = true;
            StartCoroutine(GameOver());
        }
        hud.DisableLive(lives);
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.1f);
        character.Death();

        yield return new WaitForSeconds(0.3f);
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0f;
    }

    public bool RecoverLive()
    {
        if (lives >= 3) //Maximum number of lives the player can have.
        {
            return false; //The execution of the method ends if we have the maximum number of lives(3).
        }
        lives++;
        hud.ActivateLive(lives - 1);
        return true;
    }
}
