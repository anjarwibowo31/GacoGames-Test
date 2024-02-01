using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgression", menuName = "Player Progression")]
public class PlayerProgressionSO : ScriptableObject
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
