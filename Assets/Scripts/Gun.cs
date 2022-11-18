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
    [SerializeField]
    private int lazerMaxShots;
    private int lazerShotsLeft;

    private DateTime lazerReloadStart;
    private bool lazerIsReloading;

    private DateTime lastBulletShot;

    public static event Action<float, int> OnLazerUpdate;// %of load and current count


    private Rigidbody2D shipRB;
    private Collider2D shipCollider;

    void Start()
    {
        lazerIsReloading = false;
        shipRB = ship.GetComponent<Rigidbody2D>();
        shipCollider = ship.GetComponent<Collider2D>();
        Ship.OnFire += FireBullet;
        Ship.OnLazer += FireLazer;
        lastBulletShot = DateTime.Now;
        lazerShotsLeft = lazerMaxShots;
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
        if (lazerShotsLeft == 0) return;

        Rigidbody2D lazerRB = Instantiate(lazerPrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(lazerRB.gameObject.GetComponent<Collider2D>(), shipCollider);
        lazerShotsLeft--;
    }

    


    // Update is called once per frame
    void Update()
    {
        UpdateLazer();
    }

    private void UpdateLazer()
    {
        if (lazerIsReloading)
        {
            if ((DateTime.Now - lazerReloadStart).TotalMilliseconds >= lazerCooldownMs)//loaded new shot
            {
                lazerShotsLeft++;
                lazerIsReloading = false;
            }
        }
        else
        {
            if (lazerShotsLeft < lazerMaxShots)//need to load shot
            {
                lazerIsReloading = true;
                lazerReloadStart = DateTime.Now;
            }
        }
        OnLazerUpdate?.Invoke((float)(DateTime.Now - lazerReloadStart).TotalMilliseconds / lazerCooldownMs, lazerShotsLeft);
    }

    private void OnDestroy()
    {
        Ship.OnFire -= FireBullet;
        Ship.OnLazer -= FireLazer;
    }
}
