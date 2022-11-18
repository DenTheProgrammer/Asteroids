using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ship : SpaceObject
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float turningSpeed = 0.06f;

    public bool thrust = false;

    public static event Action OnCreate;
    public static event Action OnThrust;
    public static event Action OnFire;
    public static event Action OnLazer;
    public static event Action OnDestroy;

    public static event Action<Vector2, float, float> OnMovement;//cords, angle, speed

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        OnCreate?.Invoke();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleInput();
        //Debug.Log(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    private void HandleInput()
    {
        angle = Input.GetAxis("Horizontal");
        thrust = Input.GetAxis("Vertical") > 0;
        if (Input.GetButton("Fire Bullet"))
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

        OnMovement?.Invoke(transform.position, transform.rotation.eulerAngles.z, rb.velocity.magnitude);
    }

    private void Die()
    {
        //SFX
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}
