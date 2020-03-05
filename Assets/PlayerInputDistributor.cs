﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputDistributor : MonoBehaviour
{
    private static int _currentPlayerInputNum = 0;

    public static PlayerInputDistributor instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("Can't have more than one "+name);
            Destroy(gameObject);
        }
    }
    

    public void DistributeInput(NewPlayerInput input)
    {
        if (_currentPlayerInputNum >= PlayerCharacterSpawner.instance.charactersForPlayer.Count)
        {
            print("No player for this controller to control");
            return;
        }
        PlayerCharacterSpawner.instance.charactersForPlayer[_currentPlayerInputNum].playerInput = input;
        print("distribute the generated player input object to the player "+_currentPlayerInputNum);
        _currentPlayerInputNum++;

    }
}
