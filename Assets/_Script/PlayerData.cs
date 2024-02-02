using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerData : MonoBehaviour, IDamageable
{
    public static PlayerData Instance { get; set; }

    public event Action OnGetDamage;
    public event Action OnExpChange;
    public event Action<float> SetupUI;

    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float ExpPoint { get; set; }
    public float ExpRequirement { get; set; }
    public float Damage { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        GameplaySystem.Instance.SetupDataOnStart += GameplaySystem_SetupDataOnStart;
    }

    private void GameplaySystem_SetupDataOnStart()
    {
        IsDead = false;
        Level = 1;
        Health = GameplaySystem.Instance.PlayerProgressDic[Level].healthIncrement;
        MaxHealth = GameplaySystem.Instance.PlayerProgressDic[Level].healthIncrement;
        ExpPoint = 0;
        ExpRequirement = GameplaySystem.Instance.PlayerProgressDic[Level].expPointRequirementIncrement;
        Damage = GameplaySystem.Instance.PlayerProgressDic[Level].attackIncrement;

        SetupUI?.Invoke(0);
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        OnGetDamage?.Invoke();
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public void GetExpPoint(float expAmount)
    {
        ExpPoint += expAmount;
        OnExpChange?.Invoke();

        if (ExpPoint >= ExpRequirement)
        {
            //Upgrade event
            Level++;
            NextLevelSetup(Level);
        }
    }

    public void NextLevelSetup(int level)
    {
        ExpPoint -= ExpRequirement;

        Health += GameplaySystem.Instance.PlayerProgressDic[level].healthIncrement;
        MaxHealth += GameplaySystem.Instance.PlayerProgressDic[level].healthIncrement;
        Damage += GameplaySystem.Instance.PlayerProgressDic[level].attackIncrement;
        ExpRequirement = GameplaySystem.Instance.PlayerProgressDic[level].expPointRequirementIncrement;

        SetupUI?.Invoke(ExpPoint);
    }
}