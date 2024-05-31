using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //Attribute that, whenever this script is added to a GameObject, it will create an AudioSource component automatically.

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning, More than one SoundManager in the scene");
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
