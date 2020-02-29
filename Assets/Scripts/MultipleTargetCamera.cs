﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public Vector3 velocity;
    public float smoothTime = 0.24f;

    public float minZoom = 63;
    public float maxZoom = 63f;
    public float zoomLimiter = 10f;
    // public Camera camera;
    [SerializeField]
    private Camera camera;

    private bool hasSetup;

    private void Awake()
    {
        targets = new List<Transform>();
        PlayerCharacterSpawner.onPlayerSpawnFinished += Setup;
    }

    private void FixedUpdate()
    {
        if (!hasSetup || targets.Count == 0)
        {
            return;
        }
        Move();
        Zoom();
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance()/zoomLimiter);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    public void Setup()
    {
        List<PlayerCharacter> players = PlayerCharacterSpawner.instance.charactersForPlayer;
        foreach (PlayerCharacter player in players)
        {
            targets.Add(player.transform);
        }

        hasSetup = true;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
