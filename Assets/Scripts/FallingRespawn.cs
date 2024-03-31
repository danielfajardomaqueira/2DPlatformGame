using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRespawn : MonoBehaviour
{
    public AudioClip dieSoundFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseLive();
            other.gameObject.GetComponent<CharacterController>().Respawn();
            SoundManager.Instance.PlaySound(dieSoundFX);
        }
    }
}
