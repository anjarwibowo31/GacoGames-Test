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

    private PlayerData pd;

    int i = 0;

    private void Start()
    {
        pd = PlayerData.Instance;
        pd.OnGetDamage += PDM_OnGetDamage;
        
        level.text = pd.Level.ToString();
        healthUI.maxValue = pd.Health;
        healthUI.value = pd.Health;
        expPoint.maxValue = pd.ExpRequirement;
        expPoint.value = 0;
    }

    private void PDM_OnGetDamage()
    {
        UpdateHealth();
    }

    // Event
    private void UpdateHealth()
    {
        healthUI.value = pd.Health;
    }

    private void UpdateExpPoint()
    {
        expPoint.value = pd.ExpPoint;
    }

    public void TestButton()
    {
        print(++i);
    }
}
