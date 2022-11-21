using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField]
    private Sprite shipBoostImg;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private List<Sprite> asteroidVersions;

    private SpriteRenderer shipSpriteRenderer;
    private Sprite shipImg;
    private Ship ship;
    void Start()
    {
        Ship.OnCreate += GetShipData;
        Asteroid.OnDestroy += PlayExplosion;
        Asteroid.OnSpawn += RandomizeAsteroidSprite;
        Ufo.OnDestroy += PlayExplosion;
        Ship.OnDestroy += PlayExplosion;
        
    }

    private void RandomizeAsteroidSprite(GameObject asteroidGO)
    {
        asteroidGO.GetComponent<SpriteRenderer>().sprite = asteroidVersions[UnityEngine.Random.Range(0, asteroidVersions.Count)];
    }

    private void PlayExplosion(GameObject obj)
    {
        ParticleSystem exp = Instantiate(explosion, obj.transform.position, obj.transform.rotation);
        exp.startColor = obj.GetComponent<SpriteRenderer>().color;
    }
  

    private void GetShipData()
    {
        Ship shipGO = FindObjectOfType<Ship>();
        ship = shipGO.GetComponent<Ship>();
        shipSpriteRenderer = shipGO.GetComponent<SpriteRenderer>();
        shipImg = shipSpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            shipSpriteRenderer.sprite = ship.thrust ? shipBoostImg : shipImg;
        }
    }
}
