using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenu : MonoBehaviour
{
    public Button buttonLevelTwo;

    void Start()
    {
        // Verificar si el nivel 2 est� desbloqueado
        if (PlayerPrefs.GetInt("UnlockLevel2", 0) == 1)
        {
            buttonLevelTwo.interactable = true; // Habilitar el bot�n del nivel 2
        }
        else
        {
            buttonLevelTwo.interactable = false; // Deshabilitar el bot�n del nivel 2
        }
    }
}
