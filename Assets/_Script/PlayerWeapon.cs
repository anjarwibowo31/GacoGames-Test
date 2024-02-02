using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private TrailRenderer slashVfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out EnemyController component))
        {
            component.GetDamage(PlayerData.Instance.Damage);
        }
    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
        slashVfx.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
        slashVfx.enabled = false;
    }
}
