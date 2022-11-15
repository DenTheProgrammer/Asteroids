using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MovingObject, IDestructible
{
    [SerializeField]
    private int bulletLifetimeMs;

    private DateTime firedAt;
    private int remainingTimeMs;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        firedAt = DateTime.Now;
        remainingTimeMs = bulletLifetimeMs;
    }

    protected override void Update()
    {
        if (remainingTimeMs <= (DateTime.Now - firedAt).TotalMilliseconds)
        {
            Destruct();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
