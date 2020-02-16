﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCancelProcessor : StateMachineBehaviour
{
    public List<string> animationNameToTransfer;
    public float energyConsumePerUsage = 25f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public string Process(string animationName, Animator anim)
    {
        foreach (var animationAvailable in animationNameToTransfer)
        {
            if (animationName == animationAvailable)
            {
                CharacterEnergyComponent characterEnergy = anim.GetComponent<CharacterEnergyComponent>();
                if (characterEnergy.Consume(energyConsumePerUsage))
                {
                    return animationName;
                }

                return "";
            }
        }

        return animationName;
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