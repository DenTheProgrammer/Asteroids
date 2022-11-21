using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Asteroid : SpaceObject
{
    public Size size;

    public static event Action<GameObject> OnSpawn;
    public static event Action<GameObject> OnDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.Die();
            Die();
        }else if (collision.gameObject.TryGetComponent(out Lazer lazer))
        {
            Die();
        }
    }

    private void Die()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        OnSpawn?.Invoke(gameObject);
    }

    protected override void Update()
    {
        base.Update();
    }


    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
}