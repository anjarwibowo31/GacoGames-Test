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

    public static event Action OnLoadingEvent;
    public static event Action OnPlayEventStart;
    public static event Action OnWaveTransitionEvent;

    public GameState CurrentGameState { get; set; }

    [SerializeField] private float loadingCountdownDuration = 3;

    private float loadingCountdown;

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

        loadingCountdown = loadingCountdownDuration;
    }

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                {
                    // MainMenu logic here
                    break;
                }
            case GameState.GameLoading:
                {
                    Loading(OnLoadingEvent);
                    break;
                }
            case GameState.GamePlay:
                {
                    if (GameplaySystem.Instance.CurrentWaveEnemyList.Count == 0)
                    {
                        StartCoroutine(EndWaveCoroutine());
                    }
                    break;
                }
            case GameState.GameWaveTransition:
                {
                    FindObjectOfType<PlayerController>().NormalizeAnimation();
                    Loading(OnWaveTransitionEvent);
                    break;
                }
            case GameState.GamePause:
                {
                    // GamePause logic here
                    break;
                }
            case GameState.GameOver:
                {
                    // GameOver logic here
                    break;
                }
        }

        print(CurrentGameState);
    }

    private IEnumerator EndWaveCoroutine()
    {
        yield return new WaitForSeconds(5f);
        loadingCountdown = loadingCountdownDuration;
        CurrentGameState = GameState.GameWaveTransition;
    }

    private void Loading(Action onEvent)
    {
        loadingCountdown -= Time.deltaTime;

        if (loadingCountdown >= 0)
        {
            onEvent?.Invoke();
        }
        else if (loadingCountdown < 0)
        {
            CurrentGameState = GameState.GamePlay;
            loadingCountdown = loadingCountdownDuration;
            OnPlayEventStart?.Invoke();
        }
    }

    public void LoadMainMenu()
    {
        CurrentGameState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNewGame()
    {
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
