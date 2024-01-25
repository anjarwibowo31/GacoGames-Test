using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Target>().PlayHitAnim();
        }
    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }
}
