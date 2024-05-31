using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    public static UnlockLevels Instance { get; private set; }
    public static bool unlockLevel2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UnlockLevel2()
    {
        unlockLevel2 = true;
        PlayerPrefs.SetInt("UnlockLevel2", 1); // Save Playerprefs states
    }
}
