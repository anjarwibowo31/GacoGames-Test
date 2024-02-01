using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private EnemyData enemyData;

    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;

        enemyData.OnGetDamage += EnemyData_OnGetDamage;

        //healthBar.maxValue = enemyData.Health;
    }
    private void LateUpdate()
    {
        Vector3 lookAtPos = new Vector3(transform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
        transform.LookAt(lookAtPos);

        transform.Rotate(0, 180, 0);
    }

    private void EnemyData_OnGetDamage()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        //healthBar.value = enemyData.Health;
    }
}