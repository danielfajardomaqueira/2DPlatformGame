using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public GameObject gameOverUI;
    public int TotalPoints { get { return totalPoints;  }/*,set*/ } // Propiedad solo de escritura
    public int TotalGems { get { return totalGems; } }              // Desde otro script, devuelve el valor de la variable privada(es este caso, totalGems)
    

    //----PRIVATE VARIABLES----
    private int totalPoints;
    private int lives = 3;
    private int totalGems;
    private CharacterController character;

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
    }

    public void ScorePoints(int pointsToAdd)
    {
        totalPoints = totalPoints + pointsToAdd;
        //Debug.Log(totalPoints);
        hud.UpdateScore(TotalPoints);
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
        //CharacterController.canMove = false;
        //CharacterController.canAttack = false;
        //CharacterController.canJump = false;

        yield return new WaitForSeconds(0.1f);
        character.Death();

        yield return new WaitForSeconds(0.3f);
        gameOverUI.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0f;
    }

    public bool RecoverLive()
    {
        if (lives == 3) //Maximo de vidas que puede tener el jugador.
        {
            return false; //Termina la ejecucion del metodo si tenemos el maximo de vidas(3).
        }
        hud.ActivateLive(lives);
        lives += 1;
        return true;
    }
}
