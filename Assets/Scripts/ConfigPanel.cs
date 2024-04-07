using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{

    public Toggle toggle;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer soundMixer;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void volumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void volumeSounds(float volume)
    {
        soundMixer.SetFloat("Sounds", volume);
    }
}
