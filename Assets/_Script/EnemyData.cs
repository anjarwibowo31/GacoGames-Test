using System;
using UnityEngine;

public class EnemyData : MonoBehaviour, IDamageable
{
    public event Action OnGetDamage;

    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Damage { get; set; }
    public bool IsDead { get; set; }

    private WaveManager wm;

    private void Start()
    {
        wm = WaveManager.Instance;
        Health = wm.LevelDataDictionary[wm.CurrentWave].health;
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        OnGetDamage?.Invoke();

        if (Health < MaxHealth)
        {
            IsDead = true;
        }
    }
}