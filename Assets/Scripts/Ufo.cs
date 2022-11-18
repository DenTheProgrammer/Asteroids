using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField]
    private float speedMult;
    private Transform playerTransform;


    public static event Action<Ufo> OnDestroy;
    private Rigidbody2D rb;
    void Start()
    {
        playerTransform = FindObjectOfType<Ship>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            rb.AddForce((playerTransform.position - transform.position) * speedMult);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.Die();
            Die();
        }
        else if (collision.gameObject.TryGetComponent(out Lazer lazer))
        {
            Die();
        }
    }

    private void Die()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}
