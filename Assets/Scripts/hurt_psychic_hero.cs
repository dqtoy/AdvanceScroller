﻿using System.Collections;
using UnityEngine;

public class hurt_psychic_hero : StateMachineBehaviour
{
    private Rigidbody2D rb;

    private Knockable knockable;
    private PlayerCharacter playerCharacter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        playerCharacter = animator.GetComponent<PlayerCharacter>();
        animator.GetComponent<PlayerCharacter>().canControlMovement = false;
        knockable = animator.GetComponent<Knockable>();
        rb.velocity = knockable.knockDirection;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerCharacter.isGrounded && rb.velocity.y <= 0)
            animator.SetTrigger("idle");
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