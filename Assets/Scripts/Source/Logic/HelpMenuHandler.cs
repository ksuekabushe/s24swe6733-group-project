using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenuHandler : MonoBehaviour
{
    public void BackToStartMenu() {
        UnityEngine.Debug.Log($"Back to Start Menu Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
