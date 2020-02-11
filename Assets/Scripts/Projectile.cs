﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : CollisionDetector
{
    // public float movementSpeed = 5f;
    public PlayerCharacter owner;
    public GameObject deadParticle;
    public Action<GameObject> onProjectileCollided;
    private List<GameObject> _objectsHasCollided;
    private Rigidbody2D rb;

    private bool setupFinished;

    public void Setup(PlayerCharacter _owner, float movementSpeed = 100 , float angle = 0)
    {
        owner = _owner;
        Vector2 ownerFacingDirection = owner.isFacingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        Vector2 moveDirection;
        if (owner.isFacingRight)
        {
            moveDirection = Quaternion.AngleAxis(angle, Vector3.forward) * ownerFacingDirection;
        }
        else
        {
            moveDirection = Quaternion.AngleAxis(-angle, Vector3.forward) * ownerFacingDirection;

        }
        rb.AddForce(moveDirection * (movementSpeed * 20));

        
        var projectileFacing = transform.localScale;
        if (moveDirection.x < 0)
        {
            projectileFacing = new Vector3(-projectileFacing.x, projectileFacing.y, projectileFacing.z);
            transform.localScale = projectileFacing;
            transform.Rotate(0,0,-angle);
        }
        else
        {
            projectileFacing = new Vector3(projectileFacing.x, projectileFacing.y, projectileFacing.z);
            transform.Rotate(0,0,angle);

        }
        
        setupFinished = true;
    }

    private void Awake()
    {
        onObjectCollided = FilterOwn;
        _objectsHasCollided = new List<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setupFinished)
        {
            // movementComponent.UpdateMovement();
        }
    }

    private void OnDestroy()
    {
        if (deadParticle != null)
        {
            var swordDisappear = Instantiate(deadParticle, transform.position, Quaternion.identity);
            swordDisappear.transform.localScale = transform.localScale;
            swordDisappear.transform.rotation = transform.rotation;
        }
        
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void FilterOwn(GameObject objectCollided)
    {
        if (objectCollided != owner.gameObject)
        {
            if (!_objectsHasCollided.Contains(objectCollided))
            {
                _objectsHasCollided.Add(objectCollided);
                onProjectileCollided?.Invoke(objectCollided);
            }
        }
    }
}