using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;


    private bool isPause = false;
    private CharacterController character;
    //private bool previusStateCanDash;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        character = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);

            //previusStateCanDash = character.canDash;

            CharacterController.canMove = false;
            CharacterController.canAttack = false;
            CharacterController.canJump = false;
            //CharacterController.canDash = false;
            
        }
        else
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);

            //character.canDash = previusStateCanDash;

            CharacterController.canMove = true;
            CharacterController.canAttack = true;
            CharacterController.canJump = true;
            //CharacterController.canDash = true;
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;
    }
}
