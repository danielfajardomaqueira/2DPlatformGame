using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectGems : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public int value = 1;

    [Header("Sounds")]
    public AudioClip gemSoundFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ScoreGems(value);
            Destroy(this.gameObject);
            SoundManager.Instance.PlaySound(gemSoundFX);
        }
    }
}
