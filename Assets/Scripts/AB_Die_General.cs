﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_Die_General : CharacterStateMachineBehavior
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        SwitchCollider(true);
        playerCharacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SwitchCollider(false);
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
    
    private void SwitchCollider(bool isDead)
    {
        foreach (Collider2D collider2D in playerCharacter.collidersAlive)
        {
            collider2D.enabled = !isDead;
        }

        foreach (Collider2D collider2D in playerCharacter.collidersDead)
        {
            collider2D.enabled = isDead;
        }
    }
}
