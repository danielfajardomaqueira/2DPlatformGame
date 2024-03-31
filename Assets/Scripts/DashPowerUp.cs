using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip powerUpSoundFX;

    private CharacterController character;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterController.canDash = true;
            SoundManager.Instance.PlaySound(powerUpSoundFX);

            character = other.gameObject.GetComponent<CharacterController>();
            if (character != null)
            {
                character.particlesPowerUp.Play();
            }

            Destroy(this.gameObject);
        }
    }
}
