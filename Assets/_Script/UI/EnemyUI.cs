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
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }

        healthBar.maxValue = enemyData.health;
        healthBar.value = enemyData.health;
        levelText.text = enemyData.level.ToString();
    }

    private void LateUpdate()
    {
        Vector3 lookAtPos = new Vector3(transform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
        transform.LookAt(lookAtPos);

        transform.Rotate(0, 180, 0);

        if (enemyData.IsDead)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void EnemyData_OnGetDamage(float currentHealth)
    {
        healthBar.value = currentHealth;
    }
}