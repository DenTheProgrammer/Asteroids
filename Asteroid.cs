using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Asteroid : MovingObject
{
    Size size;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out _))
        {
            Destruct();
        }
    }
    
    public enum Size
    {
        Large,
        Medium,
        Small
    }
}
