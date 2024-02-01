using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    GameLoading,
    GamePlay,
    GameWaveTransition,
    GamePause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public static event Action OnLoadingStartEvent;
    public static event Action OnLoadingEndEvent;

    public GameState CurrentGameState { get; set; }

    [SerializeField] private float loadingCountdown = 5;

    private bool isOnceCalledEventHandled = false;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            CurrentGameState = GameState.MainMenu;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            CurrentGameState = GameState.GameLoading;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) Destroy(this.gameObject);
    }

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameLoading:

                loadingCountdown -= Time.deltaTime;

                if (loadingCountdown >= 0)
                {
                    OnLoadingStartEvent?.Invoke();
                }
                else if (loadingCountdown < 0)
                {
                    CurrentGameState = GameState.GamePlay;
                    OnLoadingEndEvent?.Invoke();
                }
                break;
            case GameState.GamePlay:
                break;
            case GameState.GamePause:
                break;
            case GameState.GameOver:
                break;
        }

        print(CurrentGameState);
    }

    public void LoadMainMenu()
    {
        CurrentGameState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNewGame()
    {
        isOnceCalledEventHandled = false;
        CurrentGameState = GameState.GameLoading;
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

    public static void StopTime()
    {
        Time.timeScale = 0f;
    }

    public static void NormazlizedTime()
    {
        Time.timeScale = 1f;
    }

    // Load Scene
    // Load Saved
    // Save System
}
