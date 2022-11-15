using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingObject : MonoBehaviour, IDestructible
{

    protected Rigidbody2D rb;
    protected float angle;

    Vector2 gameDimensions;
    Camera cam;

    public event Action OnDestroy;

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
        
    }
    virtual protected void FixedUpdate()
    {
        HandleEdgeTeleportation();
    }

    private void HandleEdgeTeleportation()
    {
        Vector2 pos = transform.position;
        //Debug.Log($"x : {pos.x}, y : {pos.y}");
        if (pos.x > gameDimensions.x) pos.x = -gameDimensions.x;
        if (pos.x < -gameDimensions.x) pos.x = gameDimensions.x;
        if (pos.y > gameDimensions.y) pos.y = -gameDimensions.y;
        if (pos.y < -gameDimensions.y) pos.y = gameDimensions.y;

        transform.position = pos;
    }

    public virtual void Destruct()
    {
        //VFX,SFX in children if needed
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}

