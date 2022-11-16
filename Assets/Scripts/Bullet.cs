using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : SpaceObject
{
    [SerializeField]
    private int bulletLifetimeMs;

    private DateTime firedAt;
    private int remainingTimeMs;


    public static event Action OnDestroy;
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
            Die();
        }
    }

    public void Die()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
