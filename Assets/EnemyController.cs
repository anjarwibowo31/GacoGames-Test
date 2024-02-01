using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MoveableEntity
{
    [SerializeField] private float chaseRadius;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRadius;
    [SerializeField] private Collider swordCollider;

    private Animator anim;
    private int ANIM_BOGU_IDLE_HASH;
    private int ANIM_BOGU_ATTACK_HASH;
    private int ANIM_BOGU_DEATH_HASH;
    private int ANIM_BOGU_HIT_HASH;

    private Transform playerTransform;
    private EnemyData enemyData;
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();

        playerTransform = PlayerData.Instance.transform;
        anim = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
    }

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

        if (!IsMoveable) return;
        if (playerDistance < chaseRadius && playerDistance > attackRadius && !isAttacking)
        {
            ChasePlayer();
        }
        else if (playerDistance < attackRadius)
        {
            AttackPlayer();
        }
        else
        {
            anim.Play("Sword_Idle");
        }
    }

    private void AttackPlayer()
    {
        IsMoveable = false;
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.forward = direction;
        anim.Play("Sword_Attack");
    }

    public void EnableCollider()
    {
        swordCollider.enabled = true;
    }

    public void DisableCollider()
    {
        swordCollider.enabled = false;
    }

    private void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.forward = direction;

        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        anim.Play("Sword_Run");
    }

    public void GetHit(float damage)
    {
        IsMoveable = false;
        anim.Play("Normal_Hit");
        enemyData.GetDamage(damage);
    }

    public void FreeMove()
    {
        IsMoveable = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
