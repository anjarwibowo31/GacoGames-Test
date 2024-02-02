using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerData component))
        {
            print(enemyData.Damage);
            component.GetDamage(enemyData.Damage);
        }
    }
}
