using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Player Data")]
public class ProgressionSO : ScriptableObject
{
    public LevelData[] LevelDataArray;

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public int expPointRequirement;
        public int healthIncrement;
        public int attackIncrement;
    }
}
