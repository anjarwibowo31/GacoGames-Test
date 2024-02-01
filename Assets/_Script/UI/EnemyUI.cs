using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private EnemyData enemyData;

    private Transform mainCameraTransform;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;

        enemyData.OnGetDamage += EnemyData_OnGetDamage;
        enemyData.OnSetupData += EnemyData_OnSetupData;
    }

    private void EnemyData_OnSetupData(EnemyProgressionSO.LevelData enemyData)
    {
        healthBar.maxValue = enemyData.health;
        healthBar.value = enemyData.health;
        levelText.text = enemyData.level.ToString();
    }

    private void EnemyData_OnSetupData()
    {
    }

    private void LateUpdate()
    {
        Vector3 lookAtPos = new Vector3(transform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
        transform.LookAt(lookAtPos);

        transform.Rotate(0, 180, 0);
    }

    private void EnemyData_OnGetDamage(float currentHealth)
    {
        print($"Got damage, current Health is {currentHealth}");
        healthBar.value = currentHealth;
    }
}