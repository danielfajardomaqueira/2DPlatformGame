using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Sounds")]
    public AudioClip heartSoundFX;


    //----PRIVATE VARIABLES----
    private CharacterController character;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool liveRecovered = GameManager.Instance.RecoverLive();

            if (liveRecovered)
            {
                SoundManager.Instance.PlaySound(heartSoundFX);

                character = other.gameObject.GetComponent<CharacterController>();
                if(character != null)
                {
                    character.particlesHealth.Play();
                }

                Destroy(this.gameObject);
            }
        }
    }
}
