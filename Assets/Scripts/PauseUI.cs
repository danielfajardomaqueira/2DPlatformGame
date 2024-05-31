using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    public GameObject pauseUI;
    public AudioClip clickSoundFX;

    //----PRIVATE VARIABLES----
    static public bool isPause = false;
    private CharacterController character;
 
    void Start()
    {
        Time.timeScale = 1f;
        
        character = FindObjectOfType<CharacterController>();
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            Pause();
        }
    }

    public void Pause()
    {
        if (!CharacterController.isDead)
        {
            isPause = !isPause;

            if (isPause)
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;

                CharacterController.canMove = false;
                CharacterController.canAttack = false;
                CharacterController.canJump = false;
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;

                CharacterController.canMove = true;
                CharacterController.canAttack = true;
                CharacterController.canJump = true;
            }
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        
        isPause = false;
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        CharacterController.canMove = true;
        CharacterController.canAttack = true;
        CharacterController.canJump = true;

        SoundManager.Instance.PlaySound(clickSoundFX);
    }
}
