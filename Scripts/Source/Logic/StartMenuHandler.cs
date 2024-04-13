using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public void StartGame() {
        UnityEngine.Debug.Log($"Starting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
    public void HelpMenu() {
        UnityEngine.Debug.Log($"Loading Help Menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitGame() {
        UnityEngine.Debug.Log($"Quitting Game");
        Application.Quit();
    }
}
