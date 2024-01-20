using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private enum AnimationState
    {
        Idle,
        Run,
        Dodge,
        Attack,
        ComboAttack,
        Death,
        UseItem
    }

    private AnimationState currentState;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ChangeAnimationState(AnimationState newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case AnimationState.Idle:
                animator.Play("Combat_Idle");
                break;

            case AnimationState.Run:
                animator.Play("Combat_Run");
                break;

            case AnimationState.Dodge:
                break;

            case AnimationState.Attack:
                break;

            case AnimationState.ComboAttack:
                break;

            case AnimationState.Death:
                break;

            case AnimationState.UseItem:
                break;
        }
        
        currentState = newState;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (moveX != 0)
        {
            ChangeAnimationState(AnimationState.Run);
        }
        else
        {
            ChangeAnimationState(AnimationState.Idle);
        }
    }
}
