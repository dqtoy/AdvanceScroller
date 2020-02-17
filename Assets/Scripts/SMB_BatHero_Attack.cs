﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_BatHero_Attack : CharacterStateMachineBehavior
{
    [SerializeField] private AnimationClip groundAttack;
    [SerializeField] private AnimationClip jumpAttack;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(_animator, stateInfo, layerIndex);
        RegisterInputToNextState(new List<string> {"jump","dash"});     // TODO

        if (playerCharacter.isGrounded)
        {
            AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
 
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(jumpAttack, groundAttack));
 
            aoc.ApplyOverrides(anims);
            _animator.runtimeAnimatorController = aoc;
            playerCharacter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            playerCharacter.canControlMovement = false;
        }
        else
        {
            AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
 
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(groundAttack, jumpAttack));
 
            aoc.ApplyOverrides(anims);
            _animator.runtimeAnimatorController = aoc;
        }
        
        
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacter.GetComponent<BatHeroAttackMessager>().attackFirstStrike.StopDetectTarget();
        playerCharacter.GetComponent<BatHeroAttackMessager>().attackSecondStrike.StopDetectTarget();
        playerCharacter.canControlMovement = true;
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
