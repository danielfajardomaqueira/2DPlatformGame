using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRespawn : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Sounds")]
    public AudioClip dieSoundFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseLive();
            other.gameObject.GetComponent<CharacterController>().Respawn();
            CameraShake.Instance.ShakeCamera(5, 5, 0.5f);
            SoundManager.Instance.PlaySound(dieSoundFX);
        }
    }
}
