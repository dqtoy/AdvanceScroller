﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerInput playerInput;
    
    
    public Transform groundCheck;
    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Transform wallLeftCheck;
    public Transform wallRightCheck;
    public bool isNextToWallLeft;
    public bool isNextToWallRight;
    public LayerMask whatIsWall;
    
    [HideInInspector] public bool atEnemyLeft;
    [HideInInspector] public bool atEnemyRight;
    public LayerMask whatIsEnemy;

    [HideInInspector] public int jumpTime;
    [HideInInspector] public int maxJumpTime = 2;
    
    
    public int maxDashTimeInAir = 1;
    public int dashTimeCounter;
    public bool isDead;
    public float dashInvincibleTime = 0.25f;
    public float dashInvincibleTimeCounter;
    
    
    public List<Collider2D> collidersAlive;
    public List<Collider2D> collidersDead;
    
    [Header("Particle")]
    public GameObject groundDust;
    public GameObject deadParticle;
    public GameObject groundDustTwoWays;

    public SingleAttackComponent impactWave;
    [Space(10)]

    public Action onFacingDirectionChanged;
    public Action onPlayerStartMove;
    public Action onPlayerStartDoubleJump;
    public Action onPlayerStartJump;
    public Action onPlayerGrounded;
    public Action onPlayerStartDash;
    public Action onPlayerDodgeSucceed;
    public Action onPlayerWalkNextToWall;
    public Action onPlayerExitWall;
    
    // Animation event
    public Action onPlayerStartWallSlide;
    public Action onPlayerStopWallSlide;
    
    public bool isFacingRight => transform.localScale.x > 0;
    
    public float movementSpeed = 5f;
    public bool canControlMovement;
    [HideInInspector]public CharacterGroundMovementComponent characterGroundMovementComponent;
    [HideInInspector] public CharacterFlipComponent flipComponent;

    [HideInInspector] public GameObject chargedDagger;
    

    private void Awake()
    {
        // set up variable
        playerInput = GetComponent<PlayerInput>();
        
        
        characterGroundMovementComponent = new CharacterGroundMovementComponent(movementSpeed, transform, playerInput);
        flipComponent = new CharacterFlipComponent(transform);
        
        GetComponent<CharacterHealthComponent>().onPlayerDie = PlayerDie;
        onPlayerGrounded += ResetJumpTime;


    }
    

    private void PlayerDie()
    {
        isDead = true;
    }

    private void Update()
    {
        UpdateMovement();

        UpdateFacingDirection();

        if (dashInvincibleTimeCounter > 0)
        {
            dashInvincibleTimeCounter -= Time.deltaTime;
        }
    }

    private void UpdateFacingDirection()
    {
        var wasFacingRight = isFacingRight;
        flipComponent.Flip(playerInput.horizontalAxis);
        if (isFacingRight != wasFacingRight)
        {
            onFacingDirectionChanged?.Invoke();
        }
    }

    private void UpdateMovement()
    {
        float horizontalVelocityBefore = GetComponent<Rigidbody2D>().velocity.x;
        float previousXLocation = transform.position.x;
        characterGroundMovementComponent.UpdateMovement();
        float horizontalVelocityAfter = GetComponent<Rigidbody2D>().velocity.x;
        float afterXLocation = transform.position.x;

        if (Math.Abs(horizontalVelocityBefore) < Mathf.Epsilon && horizontalVelocityAfter > Mathf.Epsilon && isFacingRight)
        {
            if (Mathf.Abs(afterXLocation - previousXLocation) > Mathf.Epsilon)
            {
                onPlayerStartMove?.Invoke();
            }
        }
        else if (Math.Abs(horizontalVelocityBefore) < Mathf.Epsilon && horizontalVelocityAfter < -Mathf.Epsilon &&
                 !isFacingRight)
        {
            if (Mathf.Abs(afterXLocation - previousXLocation) > Mathf.Epsilon)
            {
                onPlayerStartMove?.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        updateGrounded();

        
    }

    private void updateGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (wasGrounded != isGrounded && isGrounded)
        {
            onPlayerGrounded?.Invoke();

        }

        bool wasNextToWallRight = isNextToWallRight;
        isNextToWallRight = Physics2D.OverlapCircle(wallRightCheck.position, checkRadius, whatIsWall);
        if (wasNextToWallRight != isNextToWallRight && isNextToWallRight && isFacingRight)
        {
            onPlayerWalkNextToWall?.Invoke();
        }
        else if (wasNextToWallRight != isNextToWallRight && !isNextToWallRight && isFacingRight)
        {
            onPlayerExitWall?.Invoke();
        }
        
        bool wasNextToWallLeft = isNextToWallLeft;
        isNextToWallLeft = Physics2D.OverlapCircle(wallLeftCheck.position, checkRadius, whatIsWall);
        if (wasNextToWallLeft != isNextToWallLeft && isNextToWallLeft && !isFacingRight)
        {
            onPlayerWalkNextToWall?.Invoke();
        }
        else if(wasNextToWallLeft != isNextToWallLeft && !isNextToWallLeft && !isFacingRight)
        {
            onPlayerExitWall?.Invoke();
        }
        
        atEnemyRight = Physics2D.OverlapCircle(wallLeftCheck.position, 0.2f, whatIsEnemy);
        atEnemyLeft = Physics2D.OverlapCircle(wallRightCheck.position, 0.2f, whatIsEnemy);
    }

    private void ResetJumpTime()
    {
        jumpTime = 0;
        dashTimeCounter = 0;
    }

    public void SpawnImpactWave()
    {
        impactWave.Execute();
    }
    
    public void StopImpactWave()
    {
        impactWave.StopDetectTarget();
    }

    public void PrintString(String str)
    {
        print(str);
    }
}

