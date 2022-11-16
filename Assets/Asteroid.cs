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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet;
        if (collision.gameObject.TryGetComponent<Bullet>(out bullet))
        {
            bullet.Destruct();
            Destruct();
        }
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
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Destruct()
    {
        Debug.Log("OnDestruct");
        base.Destruct();
    }

    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
}