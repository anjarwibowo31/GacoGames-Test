using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Slider healthUI;
    [SerializeField] private Slider expPoint;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI waveCount;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI expPointText;

    private PlayerData playerData;

    int i = 0;

    private void Start()
    {
        playerData = PlayerData.Instance;

        GameplaySystem.Instance.OnUpdateWave += GameplaySystem_OnUpdateWave;
        playerData.OnGetDamage += PlayerData_OnGetDamage;
        playerData.SetupUI += PlayerData_SetupUI;
        playerData.OnExpChange += PlayerData_OnExpChange;
    }

    private void PlayerData_OnExpChange()
    {
        expPoint.value = playerData.ExpPoint;
        expPointText.text = $"{playerData.ExpPoint}/{playerData.ExpRequirement}";
    }

    private void GameplaySystem_OnUpdateWave(int wave)
    {
        waveCount.text = wave.ToString();
    }

    private void PlayerData_SetupUI(float expPointRemain)
    {
        level.text = playerData.Level.ToString();
        healthUI.maxValue = playerData.MaxHealth;
        healthUI.value = playerData.Health;
        expPoint.maxValue = playerData.ExpRequirement;
        expPoint.value = expPointRemain;

        healthText.text = $"{playerData.Health}/{playerData.MaxHealth}";
        expPointText.text = $"{playerData.ExpPoint}/{playerData.ExpRequirement}";
    }

    private void PlayerData_OnGetDamage()
    {
        UpdateHealth();
    }

    // Event
    private void UpdateHealth()
    {
        healthUI.value = playerData.Health;
        healthText.text = $"{playerData.Health}/{playerData.MaxHealth}";
    }

    private void UpdateExpPoint()
    {
        expPoint.value = playerData.ExpPoint;
    }

    public void TestButton()
    {
        print(++i);
    }
}
