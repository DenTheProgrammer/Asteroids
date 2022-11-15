using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingObject : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected float angle;

    Vector2 gameDimensions;
    Camera cam;


    virtual protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    virtual protected void Start()
    {
        gameDimensions = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Debug.Log(gameDimensions);
    }

    virtual protected void Update()
    {
        Vector2 pos = transform.position;
        //Debug.Log($"x : {pos.x}, y : {pos.y}");
        if (pos.x > gameDimensions.x) pos.x = -gameDimensions.x;
        if (pos.x < -gameDimensions.x) pos.x = gameDimensions.x;
        if (pos.y > gameDimensions.y) pos.y = -gameDimensions.y;
        if (pos.y < -gameDimensions.y) pos.y = gameDimensions.y;

        transform.position = pos;
    }

    virtual protected void FixedUpdate()
    {
        
    }
}

