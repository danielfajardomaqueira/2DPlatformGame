using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public TextMeshProUGUI points;
    public TextMeshProUGUI gems;
    public GameObject[] lives;

    public void UpdateScore(int totalPoints)
    {
        points.text = totalPoints.ToString();
    }

    public void UpdateGems(int totalGems)
    {
        gems.text = totalGems.ToString();
    }

    public void DisableLive(int index)
    {
        lives[index].SetActive(false);
    }

    public void ActivateLive(int index)
    {
        lives[index].SetActive(true);
    }
}
