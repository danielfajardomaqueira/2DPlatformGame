using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Player")) && GameManager.totalGems == 3)
        {
            UnlockLevels.Instance.UnlockLevel2();
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(2);
        }
    }
}
