using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    public static GameplaySystem Instance { get; private set; }

    public int Wave { get; set; }
    public Dictionary<int, PlayerProgressionSO.LevelData> PlayerProgressDic { get; set; } = new();
    public Dictionary<int, EnemyProgressionSO.LevelData> EnemyProgressDic { get; set; } = new();

    [SerializeField] private PlayerProgressionSO playerProgressionSO;
    [SerializeField] private EnemyProgressionSO enemyProgressionSO;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParentObject;

    private Dictionary<int, GameObject> enemyDictionary = new();

    private bool wasCalled = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);

        // Put data to dictionary
        foreach (PlayerProgressionSO.LevelData levelData in playerProgressionSO.LevelDataArray)
        {
            PlayerProgressDic.Add(levelData.level, levelData);
        }

        foreach (EnemyProgressionSO.LevelData levelData in enemyProgressionSO.LevelDataArray)
        {
            EnemyProgressDic.Add(levelData.level, levelData);
        }
    }

    private void Start()
    {
        GameManager.OnLoadingStartEvent += GameManager_OnLoadingStartEvent;
    }

    private void GameManager_OnLoadingStartEvent()
    {
        Wave = 1;

        // SETUP POOLING UNTUK ENEMY
        if (wasCalled) return;
        wasCalled = true;
        for (int i = 0; i < 10; i++)
        {
            GameObject gameObject = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, enemyParentObject);
            enemyDictionary.Add(i, gameObject);
            gameObject.SetActive(false);
        }
    }
}
