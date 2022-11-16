using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float speedMult;


    public static event Action<Ufo> OnDestruction;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce((playerTransform.position - transform.position) * speedMult);
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
        OnDestruction?.Invoke(this);
        Destroy(gameObject);
    }
}
