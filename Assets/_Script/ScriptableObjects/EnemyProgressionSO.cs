using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProgression", menuName = "Enemy Progression")]
public class EnemyProgressionSO : ScriptableObject
{
    public LevelData[] LevelDataArray;

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public int health;
        public int attack;
        public int expPointIncrement;
    }
}
