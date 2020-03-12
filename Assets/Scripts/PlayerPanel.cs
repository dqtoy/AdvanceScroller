﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{ 
    public PlayerCharacter player;
    public Image ultimateBar;
    RageComponent rageComponent;

    public bool setupFinished;
    public Action onPlayerSetup;
    public Action onPlayerSetupFinish;

    
    // Start is called before the first frame update
    private void Awake()
    {
        onPlayerSetup += SetUp;
    }

    private void SetUp()
    {
        rageComponent = player.GetComponent<RageComponent>();
        setupFinished = true;
        onPlayerSetupFinish?.Invoke();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!setupFinished) return;
        
        ultimateBar.fillAmount = rageComponent.GetRagePercentage;
    }
}