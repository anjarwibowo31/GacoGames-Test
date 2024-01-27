using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private TrailRenderer slashVfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<Target>().PlayHitAnim();
            PlayerData.Instance.ExpPoint += 20f;

        }
        print(PlayerData.Instance.ExpPoint);
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
