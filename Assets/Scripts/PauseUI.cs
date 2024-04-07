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
    //private bool previusStateCanDash;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        
        character = FindObjectOfType<CharacterController>();
        pauseUI.SetActive(false);
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
            Cursor.lockState = CursorLockMode.Confined;
            
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
            Cursor.lockState = CursorLockMode.Locked;

            //character.canDash = previusStateCanDash;
            CharacterController.canMove = true;
            CharacterController.canAttack = true;
            CharacterController.canJump = true;
            //CharacterController.canDash = true;
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
