using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InjuryMaterialController : MaterialController
{
    public static InjuryMaterialController instance;

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("cannot have more than 1 "+name);
            Destroy(gameObject);
        }
        PlayerCharacterSpawner.onPlayerSpawnFinished += Register;    
    }
    
    public override void Register()
    {
        IEnumerable<CharacterHealthComponent> healthComponent = FindObjectsOfType<MonoBehaviour>().OfType<CharacterHealthComponent>();
        foreach (CharacterHealthComponent characterHealthComponent in healthComponent)
        {
            characterHealthComponent.OnTakeHit += RunBridge;
        }
    }

    public override MaterialChangeComponent GetShaderComponent(SpriteRenderer sr, Material material)
    {
        return new InjuryShaderComponent(sr, material);
    }

    private void RunBridge(CharacterHealthComponent characterHealthComponent)
    {
        Run(characterHealthComponent.GetComponent<SpriteRenderer>());
    }

}