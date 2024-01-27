﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerData : MonoBehaviour, IDamageable
{
    public static PlayerData Instance { get; set; }

    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float ExpPoint { get; set; }
    public float ExpRequirement { get; set; }
    public float Damage { get; set; }

    // test
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private ProgressionSO progressionSO;

    private Dictionary<int, ProgressionSO.LevelData> levelDataDictionary = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);


        foreach (ProgressionSO.LevelData levelData in progressionSO.LevelDataArray)
        {
            levelDataDictionary.Add(levelData.level, levelData);
        }


        Level = 1;
        Health = levelDataDictionary[Level].healthIncrement;
        MaxHealth = levelDataDictionary[Level].healthIncrement;
        ExpPoint = 0;
        ExpRequirement = levelDataDictionary[Level].expPointRequirement;
        Damage = levelDataDictionary[Level].attackIncrement;
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        // call event OnDamage
    }

    public void GetExpPoint(float expAmount)
    {
        ExpPoint += expAmount;
    }
}