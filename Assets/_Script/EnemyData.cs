using System;
using UnityEngine;

public class EnemyData : MonoBehaviour, IDamageable
{
    public event Action<float> OnGetDamage;
    public event Action<EnemyProgressionSO.LevelData> OnSetupData;

    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Damage { get; set; }
    public bool IsDead { get; set; }

    public void GetDamage(float damage)
    {
        print($"Health before damage is {Health}");

        Health -= damage;

        print($"Health after damage is {Health}");

        OnGetDamage.Invoke(Health);

        if (Health < MaxHealth)
        {
            IsDead = true;
        }
    }

    public void SetupData(EnemyProgressionSO.LevelData enemyData)
    {
        Level = enemyData.level;
        MaxHealth = enemyData.health;
        Health = enemyData.health;
        Damage = enemyData.attack;
        IsDead = false;

        OnSetupData.Invoke(enemyData);
    }
}