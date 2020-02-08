﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump_psychic_hero : CharacterStateMachineBehavior
{
    [SerializeField] private float jumpForce = 10f;
    private PlayerCharacter playerCharacter;
    private Rigidbody2D rb;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
        RegisterInputToNextState(new List<string> {"double jump", "dash", "jump attack","skill3"});
        playerCharacter = animator.GetComponent<PlayerCharacter>();
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        playerCharacter.canMove = true;

        animator.GetComponent<PlayerCharacter>().characterGroundMovementComponent.UpdateMovement();

        
        
        
        if (rb.velocity.y < 0)
        {
            animator.SetTrigger("fall down");
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


