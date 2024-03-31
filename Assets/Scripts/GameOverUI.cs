using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{



    public void Restart()
    {
        CharacterController.canDash = false;
        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
        CharacterController.isDead = false;

        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //SceneManager.LoadScene(0);
    }
}
