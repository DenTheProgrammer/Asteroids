using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ship : SpaceObject
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float turningSpeed = 0.06f;

    bool thrust = false;

    public event Action OnThrust;
    public event Action OnFire;
    public event Action OnLazer;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleInput();
        //Debug.Log(angle);
    }

    private void HandleInput()
    {
        angle = Input.GetAxis("Horizontal");
        thrust = Input.GetAxis("Vertical") > 0;
        if (Input.GetButtonDown("Fire Bullet"))
        {
            OnFire?.Invoke();
        }
        if (Input.GetButtonDown("Fire Lazer"))
        {
            OnLazer?.Invoke();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        HandleMovement();
    }


    private void HandleMovement()
    {
        rb.AddTorque(-angle * turningSpeed);
        if (thrust)
        {
            OnThrust?.Invoke();
            rb.AddForce(transform.right * speed);
        }
    }


}
