﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_Dash_General : CharacterStateMachineBehavior
{
    [SerializeField] private float dashSpeed = 8;
    private bool isDashReversed;
    [SerializeField] private float dashDuration = 0.4f;
    private bool dashRight;
    private bool hasActivatedBulletTime;
    private bool hitEnemy;

    private float dashTimeCounter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(_animator, stateInfo, layerIndex);
        hitEnemy = false;
        hasActivatedBulletTime = false;
        dashTimeCounter = dashDuration;
        if (!playerCharacter.isGrounded)
        {
            playerCharacter.dashTimeCounter++;
        }

        dashRight = playerCharacter.isFacingRight;
        if (playerCharacter.isFacingRight && playerCharacter.GetComponent<PlayerInput>().horizontalAxis < 0)
        {
            Vector3 localScale = playerCharacter.transform.localScale;
            localScale.x = -1;
            playerCharacter.onFacingDirectionChanged?.Invoke();
            playerCharacter.transform.localScale = localScale;
            
            isDashReversed = true;
        }
        else if (!playerCharacter.isFacingRight && playerCharacter.GetComponent<PlayerInput>().horizontalAxis > 0)
        {
            Vector3 localScale = playerCharacter.transform.localScale;
            localScale.x = 1;
            playerCharacter.onFacingDirectionChanged?.Invoke();
            playerCharacter.transform.localScale = localScale;
            
            isDashReversed = true;
        }
        else
        {
            isDashReversed = false;
        }

        playerCharacter.dashInvincibleTimeCounter = playerCharacter.dashInvincibleTime;
        // event
        playerCharacter.onPlayerStartDash?.Invoke();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(_animator, stateInfo, layerIndex);
        dashTimeCounter -= Time.deltaTime;
        if (dashTimeCounter <= 0)
        {
            _animator.SetTrigger("idle");
        }

        _animator.GetComponent<PlayerCharacter>().canControlMovement = false;
        Rigidbody2D rb = _animator.GetComponent<Rigidbody2D>();

        if (!isDashReversed)
        {
            if (playerCharacter.isFacingRight && playerCharacter.atEnemyLeft)
            {
                rb.velocity = Vector3.zero;
                hitEnemy = true;
            }
            else if (!playerCharacter.isFacingRight && playerCharacter.atEnemyRight)
            {
                rb.velocity = Vector3.zero;
                hitEnemy = true;
            }
        }
        

        if (!hitEnemy)
        {
            if (dashRight)
            {
                rb.velocity = Vector2.right * (dashSpeed * (isDashReversed ? -1 : 1));
            }
            else
            {
                rb.velocity = -Vector2.right * (dashSpeed * (isDashReversed ? -1 : 1));
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerCharacter>().canControlMovement = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    public void ActivateDodgeBulletTime()
    {
        if (!hasActivatedBulletTime)
        {
            BulletTimeManager.instance.Register(0.2f);
            hasActivatedBulletTime = true;
        }
    }
}