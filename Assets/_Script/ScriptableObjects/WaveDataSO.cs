using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class WaveDataSO : ScriptableObject
{
    public SingleWaveData[] SingleWaveDataArray;

    [System.Serializable]
    public class SingleWaveData
    {
        public int wave;
        public int enemyLevel;
        public int enemyCount;
    }
}