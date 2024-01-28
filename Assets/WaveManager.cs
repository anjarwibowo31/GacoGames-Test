using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Loading,
    Wave
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; set; }

    public State GameState { get; set; }
    public int CurrentWave { get; set; }

    [SerializeField] private EnemyProgressionSO enemyProgression;

    public Dictionary<int, EnemyProgressionSO.LevelData> LevelDataDictionary { get; set; } = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);

        CurrentWave = 1;

        foreach (EnemyProgressionSO.LevelData levelData in enemyProgression.LevelDataArray)
        {
            LevelDataDictionary.Add(levelData.level, levelData);
        }
        GameState = State.Loading;
    }

    void Update()
    {
        switch (GameState)
        {
            case State.Loading:
                break;
            case State.Wave:
                break;
        }
    }
}
