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

    private PlayerData PDM;

    int i = 0;

    private void Start()
    {
        PDM = PlayerData.Instance;
        
        level.text = PDM.Level.ToString();
        healthUI.maxValue = PDM.Health;
        healthUI.value = PDM.Health;
        expPoint.maxValue = PDM.ExpRequirement;
        expPoint.value = 0;
    }

    // Event
    private void UpdateHealth()
    {
        healthUI.value = PDM.Health;
    }

    private void UpdateExpPoint()
    {
        expPoint.value = PDM.ExpPoint;
    }

    public void TestButton()
    {
        print(++i);
    }
}
