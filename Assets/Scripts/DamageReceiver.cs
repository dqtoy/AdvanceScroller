﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class DamageReceiver : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private StunComponent stunComponent;
    private CharacterHealthComponent characterHealthComponent;
    private Knockable knockableComponent;
    private PushComponent pushComponent;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        stunComponent = GetComponent<StunComponent>();
        characterHealthComponent = GetComponent<CharacterHealthComponent>();
        knockableComponent = GetComponent<Knockable>();
        pushComponent = GetComponent<PushComponent>();
    }

    public void Analyze(DamageData damageData, Transform damageOwner)
    {
        if (damageData.pushPower > 0)
        {
            if (damageData.pushType == PushType.SpecificAngleOnly)
            {
                pushComponent.Push(damageOwner, damageData.pushPower, damageData.offSetDegree, damageData.pushDistance);
            }
            else if (damageData.pushType == PushType.AccordingToRelativePosition)
            {
                pushComponent.Push(damageOwner, damageData.pushPower, damageData.pushDistance);

            }
        }

        if (damageData.hitStunPower > 0)
        {
            stunComponent.SetStunTime(damageData.hitStunPower);
        }

        if (damageData.damage > 0)
        {
            characterHealthComponent.TakeDamage(damageData,damageOwner,true);
        }

        if (damageData.launcherHorizontalForce > 0 || damageData.launcherVerticalForce > 0)
        {
            knockableComponent.KnockUp(damageData.launcherHorizontalForce, damageData.launcherVerticalForce, damageOwner);
        }
    }    
}