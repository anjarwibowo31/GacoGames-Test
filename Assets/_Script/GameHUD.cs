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

    private PlayerDataManager PDM;

    private void Start()
    {
        PDM = PlayerDataManager.Instance;


        healthUI.maxValue = PDM.Health;
        expPoint.maxValue = PDM.ExpRequirement;
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
}
