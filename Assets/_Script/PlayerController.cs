using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Weapon weapon;

    private Animator animator;
    private bool canMove;
    private bool canRotate;

    private int ANIM_SPEED_HASH;
    private int ANIM_ATTACK_HASH;
    private int ANIM_DODGE_HASH;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        ANIM_SPEED_HASH = Animator.StringToHash("Speed");
        ANIM_ATTACK_HASH = Animator.StringToHash("Attack");
        ANIM_DODGE_HASH = Animator.StringToHash("Dodge");
    }

    void Update()
    {
        HandleMovement();
        HandleDodge();
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger(ANIM_ATTACK_HASH);
    }

    private void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            animator.SetTrigger(ANIM_DODGE_HASH);
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
    }
}