using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField]
    private Sprite shipBoostImg;

    private SpriteRenderer shipSpriteRenderer;
    private Sprite shipImg;
    private Ship ship;
    void Start()
    {
        Ship.OnCreate += GetShipData;
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
