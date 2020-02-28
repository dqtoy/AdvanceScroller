﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_AxeHero_FallDown : CharacterStateMachineBehavior
{
    private PlayerCharacter messagingSystem;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(_animator, stateInfo,layerIndex);
        messagingSystem = _animator.GetComponent<PlayerCharacter>();
        
        playerCharacter.onPlayerWalkNextToWall += TransferToWallSlide;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(_animator, stateInfo, layerIndex);
        if (messagingSystem.isGrounded && characterAnimator.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            _animator.SetTrigger("idle");
        }
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo,layerIndex);
        playerCharacter.onPlayerWalkNextToWall -= TransferToWallSlide;

    }
}
