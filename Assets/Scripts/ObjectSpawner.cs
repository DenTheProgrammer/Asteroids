using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;
    [SerializeField]
    private GameObject ufoPrefab;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private int startAsteroidCount;
    [SerializeField]
    private int ScoreToSpawnUfo;
    [SerializeField]
    public static List<GameObject> aliveEnemies;


    private int scoreSinceLastUFO;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStart += SpawnPlayer;
        GameManager.OnScoreUpdate += CheckUfoCondition;
        GameManager.OnNextLvl += GenerateLevel;
        aliveEnemies = new List<GameObject>();
        Asteroid.OnDestroy += SplitAsteroid;
        Ufo.OnDestroy += RemoveUfoFromList;
    }


    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.Euler(0,0,90));
    }
    private void CheckUfoCondition(int addedScore, int totalScore)
    {
        scoreSinceLastUFO += addedScore;

        if (scoreSinceLastUFO >= ScoreToSpawnUfo)
        {
            scoreSinceLastUFO -= ScoreToSpawnUfo;
            SpawnUFO();
        }
    }

    private void RemoveUfoFromList(Ufo ufo)
    {
        aliveEnemies.Remove(ufo.gameObject);
    }


    private void GenerateLevel(int difficulty)
    {
        ClearLevel();
        for (int i = 0; i < startAsteroidCount + (difficulty - 1); i++)
        {
            Vector2 randDirection = UnityEngine.Random.insideUnitCircle.normalized;
            float randDist = UnityEngine.Random.Range(3, 8);
            float randSpeed = UnityEngine.Random.Range(0.5f, 3);
            Vector2 velocity = (randDirection * randSpeed);

            SpawnAsteroid(Asteroid.Size.Large, randDist * randDirection, Quaternion.identity, velocity);
        }
    }
    private void SplitAsteroid(Asteroid asteroid)
    {
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
        aliveEnemies.Remove(asteroid.gameObject);
    }

    private GameObject SpawnUFO()
    {
        Vector2 randDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float randDist = UnityEngine.Random.Range(9, 10);

        GameObject ufo = Instantiate(ufoPrefab, randDirection*randDist, Quaternion.identity);

        aliveEnemies.Add(ufo);
        return ufo;
    }


    private GameObject SpawnAsteroid(Asteroid.Size size, Vector2 pos, Quaternion rotation, Vector2 initSpeed)
    {
        GameObject asteroid = Instantiate(asteroidPrefab, pos, rotation);

        Asteroid asteroidClass = asteroid.GetComponent<Asteroid>();
        asteroidClass.size = size;
        Rigidbody2D asteroidRB = asteroid.GetComponent<Rigidbody2D>();
        asteroidRB.velocity = initSpeed;

        aliveEnemies.Add(asteroid);
        return asteroid;
    }

    private void ClearLevel()
    {
        foreach (var enemy in aliveEnemies)
        {
            Destroy(enemy);
        }
        aliveEnemies.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Alive enemies count: {aliveEnemies.Count}");
    }
}
