﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_WallSlide_General : CharacterStateMachineBehavior
{
    private bool isRightWallSlide;
    [SerializeField] private float fallDownSpeed = 5f;

    private Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerCharacter.onPlayerStartWallSlide?.Invoke();
        RegisterInputToNextState(new List<string>{"skill3", "dash","jump"});
        playerCharacter.onPlayerExitWall += PlayerLeavesWall;
        isRightWallSlide = playerCharacter.isNextToWallRight && playerCharacter.isFacingRight;
            
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        rb.velocity = Vector2.down * fallDownSpeed;
        
        if (playerCharacter.isNextToWallLeft && playerCharacter.isFacingRight)
        {
            animator.SetTrigger("fall down");
        }
        
        
        if (playerCharacter.isNextToWallRight && !playerCharacter.isFacingRight)
        {
            animator.SetTrigger("fall down");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        playerCharacter.onPlayerStopWallSlide?.Invoke();
        playerCharacter.onPlayerExitWall -= PlayerLeavesWall;
    }

    public void PlayerLeavesWall()
    {
        characterAnimator.SetTrigger("idle");
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
}
