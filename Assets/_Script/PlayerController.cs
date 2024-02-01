using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MoveableEntity
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float checkRadius = 6f;
    [SerializeField] private Weapon weapon;
    [SerializeField] private int playerLayer;
    [SerializeField] private int enemyLayer;
    [SerializeField] private LayerMask enemyLayerMask;

    private Animator animator;
    private bool canMove;
    private bool canRotate;
    private bool intoCombo;

    private int ANIM_SPEED_HASH;
    private int ANIM_ATTACK_HASH;
    private int ANIM_DODGE_HASH;
    private int ANIM_DO_COMBO_HASH;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        ANIM_SPEED_HASH = Animator.StringToHash("Speed");
        ANIM_ATTACK_HASH = Animator.StringToHash("Attack");
        ANIM_DODGE_HASH = Animator.StringToHash("Dodge");
        ANIM_DO_COMBO_HASH = Animator.StringToHash("DoCombo");
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        //if (PlayerData.Instance.IsDead)
        //{
        //    animator.Play("ANIM_Claris_Die_01");
        //    return;
        //}

        if (!IsMoveable) return;
        HandleMovement();
        HandleDodge();
        HandleAttack();
    }

    private void HandleAttack()
    {
        Collider[] enemiesNearby = Physics.OverlapSphere(transform.position, checkRadius, enemyLayerMask);
        Transform nearestEnemy = FindNearestEnemy(enemiesNearby);

        if (Input.GetMouseButtonDown(0))
        {
            if (nearestEnemy != null)
            {
                Vector3 moveDirection = (nearestEnemy.position - transform.position).normalized;
                transform.forward = moveDirection;
            }

            if (intoCombo)
                animator.SetTrigger(ANIM_DO_COMBO_HASH);
            else
                animator.SetTrigger(ANIM_ATTACK_HASH);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.yellow;
    }

    Transform FindNearestEnemy(Collider[] enemies)
    {
        Transform nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    private void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger(ANIM_DODGE_HASH);
            Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            if (canMove)
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (canRotate)
                transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        animator.SetFloat(ANIM_SPEED_HASH, moveDirection.magnitude);
    }


    // Method called in animation event
    private void EnableMovementAndRotation()
    {
        canMove = true;
        canRotate = true;
    }

    private void DisableMovementAndRotation()
    {
        canMove = false;
        canRotate = false;
    }

    private void EnableAttackBehaviour()
    {
        DisableMovementAndRotation();
        weapon.EnableCollider();
    }

    private void DisableAttackBehaviour()
    {
        EnableMovementAndRotation();
        weapon.DisableCollider();
        DisableIntoCombo();

        animator.ResetTrigger(ANIM_ATTACK_HASH);
        animator.ResetTrigger(ANIM_DO_COMBO_HASH);
    }

    private void EnableIntoCombo()
    {
        intoCombo = true;
    }

    private void DisableIntoCombo()
    {
        intoCombo = false;
    }

    private void ResetIgnoreLayerCollision()
    {
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }
}