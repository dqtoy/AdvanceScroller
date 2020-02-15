﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCharacter))]
public class HealthComponent : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private static readonly int Die = Animator.StringToHash("die");

    
    private bool isTimeFreezed;
    private float freezeTimeCounter;
    private float maxBulletTime = 0.4f;
    private float bulletTime;
    private CameraShake cameraShake;
    public bool isPlayerDead => currentHealth <= 0;
    public PlayerCharacter playerCharacter;
    
    
    public FloatAction onHealthChanged;
    public Action onTakeDamage;
    public Action onPlayerDie;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (Camera.main != null) cameraShake = Camera.main.GetComponent<CameraShake>();
        playerCharacter = GetComponent<PlayerCharacter>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
    }

    // Start is called before the first frame update
    public void TakeDamage(int amount, bool canTimeFreezed = true)
    {
        if (playerCharacter.dashInvincibleTimeCounter > 0)
        {
            playerCharacter.onPlayerDodgeDamage?.Invoke();
        }
        currentHealth = Mathf.Clamp(currentHealth-amount, 0, 100);
        if (currentHealth == 0)
        {
                onPlayerDie?.Invoke();
        }

        float percentage = (float) amount / maxHealth;

        float strength = Mathf.Clamp01(percentage);

        StartCoroutine(cameraShake.Shake(.15f, .1f));

        bulletTime = maxBulletTime * strength;
        
        if (canTimeFreezed)
        {
            BulletTimeManager.instance.Register(bulletTime);
        }

        onHealthChanged?.Invoke(currentHealth);
        onTakeDamage?.Invoke();
    }

    public void HitFreeze()
    {
        Time.timeScale = 0.01f;
        isTimeFreezed = true;
    }
    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth+amount, 0, 100);
        onHealthChanged?.Invoke(currentHealth);
    }
}

[Serializable]
public class FloatAction : UnityEvent<float>
{
    
}