using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MovingObject
{
    [SerializeField]
    float speed = 1f;
    float turningSpeed = 20f;
    bool thrust = false;

    public event Action onThrust;
    public event Action onShot;

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
        angle = Input.GetAxis("Horizontal");
        thrust = Input.GetAxis("Vertical") > 0;
        //Debug.Log(angle);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.AddTorque(-angle * turningSpeed);
        Thrust();
    }

    private void Thrust()
    {
        if (thrust)
        {
            onThrust?.Invoke();
            rb.AddForce(transform.right * speed);
        }
    }
}
