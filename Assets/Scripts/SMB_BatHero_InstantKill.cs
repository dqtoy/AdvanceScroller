﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_BatHero_InstantKill : CharacterStateMachineBehavior
{
    private Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerCharacter.GetComponent<UniqueSkillPauseComponent>().ShowOff();
        rb = playerCharacter.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
       
        animator.GetComponent<BatHeroAttackMessager>().closeYourEyes.StopDetectTarget();
        animator.GetComponent<BatHeroAttackMessager>().instantKill.StopDetectTarget();
        playerCharacter.GetComponent<UniqueSkillPauseComponent>().UniqueSkillEnd();
        rb.gravityScale = 1;
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