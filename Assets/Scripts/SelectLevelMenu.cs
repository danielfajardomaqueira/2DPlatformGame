using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenu : MonoBehaviour
{
    public Button buttonLevelTwo;

    void Start()
    {
        // Check if level 2 is unlocked
        if (PlayerPrefs.GetInt("UnlockLevel2", 0) == 1)
        {
            buttonLevelTwo.interactable = true; // Habilitar el botón del nivel 2
        }
        else
        {
            buttonLevelTwo.interactable = false; // Deshabilitar el botón del nivel 2
        }
    }
}
