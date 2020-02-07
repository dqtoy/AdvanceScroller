﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_down_psychic_hero : CharacterStateMachineBehavior
{
    private PsychicHeroMessagingSystem messagingSystem;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
        RegisterInputToNextState(new List<string>{"jump attack", "skill3"});
        messagingSystem = animator.GetComponent<PsychicHeroMessagingSystem>();
        if (!messagingSystem.hasDoubleJump)
        {
            RegisterInputToNextState("double jump");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (messagingSystem.isGrounded)
        {
            animator.SetTrigger("idle");
        }
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
