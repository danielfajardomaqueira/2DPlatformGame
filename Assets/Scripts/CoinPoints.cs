using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoints : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public int value = 1;

    [Header("Sounds")]
    public AudioClip coinSoundFX;
    //public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ScorePoints(value);
            SoundManager.Instance.PlaySound(coinSoundFX);
            Destroy(this.gameObject);
        }
    }
}
