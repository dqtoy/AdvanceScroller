﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class DamageReceiver : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private StunComponent stunComponent;
    private HealthComponent healthComponent;
    private Knockable knockableComponent;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        stunComponent = GetComponent<StunComponent>();
        healthComponent = GetComponent<HealthComponent>();
        knockableComponent = GetComponent<Knockable>();
    }

    public void Analyze(DamageData damageData, Transform damageOwner)
    {
        if (damageData.pushPower > 0)
        {
            // TODO
        }

        if (damageData.hitStunPower > 0)
        {
            stunComponent.SetStunTime(damageData.hitStunPower);
        }

        if (damageData.damage > 0)
        {
            healthComponent.TakeDamage(damageData.damage);
        }

        if (damageData.launcherHorizontalForce > 0 || damageData.launcherVerticalForce > 0)
        {
            knockableComponent.KnockUp(damageData.launcherHorizontalForce, damageData.launcherVerticalForce, damageOwner);
        }
    }    
}