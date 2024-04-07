using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public AudioClip clickSoundFX;

    public void Restart()
    {
        CharacterController.canDash = false;
        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
        CharacterController.isDead = false;

        SoundManager.Instance.PlaySound(clickSoundFX);

        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        CharacterController.canDash = false;
        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
        CharacterController.isDead = false;


        SceneManager.LoadScene(0);
    }
}
