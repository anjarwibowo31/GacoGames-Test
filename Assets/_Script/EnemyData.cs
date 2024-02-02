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
    public float ExpPointIncrement { get; set; }
    public bool IsDead { get; set; }

    private void Start()
    {
        
    }

    public void GetDamage(float damage)
    {
        Health -= damage;

        OnGetDamage.Invoke(Health);

        if (Health <= 0)
        {
            PlayerData.Instance.GetExpPoint(ExpPointIncrement);
            GameplaySystem.Instance.CurrentWaveEnemyList.Remove(this.gameObject);
            IsDead = true;
        }
    }

    public void SetupData(EnemyProgressionSO.LevelData enemyData)
    {
        Level = enemyData.level;
        MaxHealth = enemyData.health;
        Health = enemyData.health;
        Damage = enemyData.attack;
        ExpPointIncrement = enemyData.expPointIncrement;
        IsDead = false;

        OnSetupData.Invoke(enemyData);
    }
}