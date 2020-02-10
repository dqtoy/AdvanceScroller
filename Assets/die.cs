﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : CharacterStateMachineBehavior
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        foreach (Collider2D collider2D in playerCharacter.collidersAlive)
        {
            collider2D.enabled = false;
        }

        foreach (Collider2D collider2D in playerCharacter.collidersDead)
        {
            collider2D.enabled = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (Collider2D collider2D in playerCharacter.collidersAlive)
        {
            collider2D.enabled = true;
        }

        foreach (Collider2D collider2D in playerCharacter.collidersDead)
        {
            collider2D.enabled = false;
        }
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
