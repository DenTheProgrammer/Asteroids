using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;
    [SerializeField]
    private GameObject ufoPrefab;
    [SerializeField]
    private int startAsteroidCount;

    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gameManager;
        gm.OnGameStart += SpawnInitialAsteroids;
        
    }

    private void SpawnInitialAsteroids()
    {
        for (int i = 0; i < startAsteroidCount; i++)
        {
            Vector2 randDirection = UnityEngine.Random.insideUnitCircle.normalized;
            float randDist = UnityEngine.Random.Range(3, 8);
            float randSpeed = UnityEngine.Random.Range(0.5f, 3);
            Vector2 velocity = (randDirection * randSpeed);

            SpawnAsteroid(Asteroid.Size.Large, randDist * randDirection, Quaternion.identity, velocity);
        }

    }
    private void SplitAsteroid(SpaceObject ast)
    {
        Asteroid asteroid = (Asteroid)ast;
        if (asteroid.size != Asteroid.Size.Small)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 randDirection = UnityEngine.Random.insideUnitCircle.normalized;
                float randSpeed = UnityEngine.Random.Range(1f, 2.5f);

                GameObject smallerAsteroid = SpawnAsteroid(asteroid.size - 1, asteroid.transform.position, Quaternion.identity, randDirection * randSpeed);
                Asteroid smallerAsteroidClass = smallerAsteroid.GetComponent<Asteroid>();
                switch (smallerAsteroidClass.size)
                {
                    case Asteroid.Size.Small:
                        smallerAsteroid.transform.localScale *= 0.35f;
                        break;
                    case Asteroid.Size.Medium:
                        smallerAsteroid.transform.localScale *= 0.7f;
                        break;
                }
            }
        }
    }


    private GameObject SpawnAsteroid(Asteroid.Size size, Vector2 pos, Quaternion rotation, Vector2 initSpeed)
    {
        GameObject asteroid = Instantiate(asteroidPrefab, pos, rotation);

        Asteroid asteroidClass = asteroid.GetComponent<Asteroid>();
        asteroidClass.size = size;
        Rigidbody2D asteroidRB = asteroid.GetComponent<Rigidbody2D>();
        asteroidRB.velocity = initSpeed;

        asteroidClass.OnDestroy += SplitAsteroid;
        return asteroid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
