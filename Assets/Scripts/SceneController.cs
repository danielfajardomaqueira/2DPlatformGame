using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        PauseUI.isPause = false;
        SceneManager.LoadScene(0);
    }

    public void LevelOne()
    {
        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
        CharacterController.canDash = false;

        GameManager.totalGems = 0;

        SceneManager.LoadScene(3);
    }

    public void LevelTwo()
    {
        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
        CharacterController.canDash = false;

        GameManager.totalGems = 0;

        SceneManager.LoadScene(4);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(1);
    }
    

}
