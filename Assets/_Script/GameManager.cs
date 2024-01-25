using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadSavedGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Save()
    {

    }

    // Load Scene
    // Load Saved
    // Save System
}
