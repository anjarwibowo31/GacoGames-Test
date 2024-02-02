using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    public static GameplaySystem Instance { get; private set; }

    public event Action<int> GetSpawnLocation;
    public event Action SetupDataOnStart;
    public event Action<int> OnUpdateWave;

    public int CurrentWave { get; set; }
    public static List<Transform> SpawnLocationList { get; set; } = new();
    public Dictionary<int, WaveDataSO.SingleWaveData> WaveDataDic { get; set; } = new();
    public Dictionary<int, PlayerProgressionSO.LevelData> PlayerProgressDic { get; set; } = new();
    public Dictionary<int, EnemyProgressionSO.LevelData> EnemyProgressDic { get; set; } = new();
    public List<GameObject> CurrentWaveEnemyList { get; set; } = new();


    [SerializeField] private WaveDataSO waveDataSO;
    [SerializeField] private PlayerProgressionSO playerProgressionSO;
    [SerializeField] private EnemyProgressionSO enemyProgressionSO;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParentObject;
    [SerializeField] private EnemySpawnLocation enemySpawnLocation;

    private Dictionary<int, GameObject> enemyDictionary = new();

    private bool wasCalled = false;
    private int maxWave = 10;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);

        // Put data to dictionary

        foreach (WaveDataSO.SingleWaveData singleWaveData in waveDataSO.SingleWaveDataArray)
        {
            WaveDataDic.Add(singleWaveData.wave, singleWaveData);
        }

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
        GameManager.OnLoadingEvent += GameManager_OnLoadingEvent;
        GameManager.OnWaveTransitionEvent += GameManager_OnWaveTransitionEvent;
        GameManager.OnPlayEventStart += GameManager_OnPlayEvent;

        CurrentWave = 1;
        SetupDataOnStart?.Invoke();
        maxWave = waveDataSO.SingleWaveDataArray.Length;
    }

    private void Update()
    {
        enemySpawnLocation.transform.position = PlayerData.Instance.transform.position;
    }

    private void GameManager_OnPlayEvent()
    {
        wasCalled = false;
    }

    private void GameManager_OnLoadingEvent()
    {
        // Update() loop prevention
        if (wasCalled) return;
        wasCalled = true;

        // SET UP POOLING UNTUK ENEMY
        for (int i = 0; i < 10; i++)
        {
            GameObject gameObject = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, enemyParentObject);
            enemyDictionary.Add(i, gameObject);
            gameObject.SetActive(false);
        }

        SpawnEnemy(CurrentWave);
    }

    private void GameManager_OnWaveTransitionEvent()
    {
        // Update() loop prevention
        if (wasCalled) return;
        wasCalled = true;

        CurrentWave++;
        OnUpdateWave?.Invoke(CurrentWave);
        if (CurrentWave <= WaveDataDic.Count)
        {
            foreach (GameObject gameObject in enemyDictionary.Values) gameObject.SetActive(false);
            SpawnEnemy(CurrentWave);
        }
    }

    private void SpawnEnemy(int currentWave)
    {
        CurrentWaveEnemyList.Clear();

        int enemyCount = WaveDataDic[currentWave].enemyCount;
        int enemyLevel = WaveDataDic[currentWave].enemyLevel;

        EnemyProgressionSO.LevelData enemyData = EnemyProgressDic[enemyLevel];

        GetSpawnLocation?.Invoke(WaveDataDic[currentWave].enemyCount);

        List<Transform> tempLocList = new(SpawnLocationList);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = enemyDictionary[i];
            enemy.transform.position = tempLocList[i].position;
            enemy.SetActive(true);
            CurrentWaveEnemyList.Add(enemy);

            enemy.GetComponent<EnemyData>().SetupData(enemyData);
            enemy.GetComponent<Animator>().Play("Sword_Idle");
        }
    }
}
