using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private EnemyData enemyData;

    private void Start()
    {
        enemyData.OnGetDamage += EnemyData_OnGetDamage;

        healthBar.maxValue = enemyData.Health;
    }

    private void Update()
    {
        if (healthBar.maxValue <= 5)
        {
            healthBar.maxValue = enemyData.Health;
        }
    }
    private void EnemyData_OnGetDamage()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = enemyData.Health;
    }
}