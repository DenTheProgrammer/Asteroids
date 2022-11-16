using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class Gun : MonoBehaviour
{

    [SerializeField]
    private Ship ship;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject lazerPrefab;
    [SerializeField]
    private int bulletCooldownMs;
    [SerializeField]
    private float bulletInitialVelocity;
    [SerializeField]
    private int lazerCooldownMs;


    private DateTime lastBulletShot;
    private DateTime lastLazerShot;

    private Rigidbody2D shipRB;
    private Collider2D shipCollider;

    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody2D>();
        shipCollider = ship.GetComponent<Collider2D>();
        ship.OnFire += FireBullet;
        ship.OnLazer += FireLazer;
        lastBulletShot = DateTime.Now;
        lastLazerShot = DateTime.Now;
    }
    private void FireBullet()
    {
        if ((DateTime.Now - lastBulletShot).TotalMilliseconds < bulletCooldownMs) return;

        Rigidbody2D bulletRB = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(bulletRB.gameObject.GetComponent<Collider2D>(), shipCollider);
        bulletRB.velocity = //shipRB.velocity + 
            ((Vector2)ship.transform.right.normalized * bulletInitialVelocity);
        lastBulletShot = DateTime.Now;

    }

    private void FireLazer()
    {
        if ((DateTime.Now - lastLazerShot).TotalMilliseconds < lazerCooldownMs) return;

        Rigidbody2D lazerRB = Instantiate(lazerPrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(lazerRB.gameObject.GetComponent<Collider2D>(), shipCollider);
        lastLazerShot = DateTime.Now;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
