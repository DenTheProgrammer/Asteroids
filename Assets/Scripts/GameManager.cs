using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _state;
    public GameState gameState { set { 
        _state = value;
            Debug.Log($"Game state changed to {value}");
        } 
        get { return _state; } }


    private int _score;
    private int difficultyLvl;
    public static GameManager gameManager;

    public static event Action OnGameStart;
    public static event Action<int> OnNextLvl;
    public static event Action OnGameOver;
    public static event Action<int, int> OnScoreUpdate;

    private void Awake()
    {
        gameManager = this;
        gameState = GameState.WaitingToStart;
    }

    private void Start()
    {
        Ship.OnDestroy += EndGame;
        Asteroid.OnDestroy += ScoreAsteroid;
        Ufo.OnDestroy += ScoreUfo;
    }

    private void AddScore(int amount)
    {
        _score += amount;
        OnScoreUpdate?.Invoke(amount, _score);
        Debug.Log($"SCORE +{amount} | TOTAL : {_score}");
    }
    private void EndGame()
    {
        gameState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    private void ScoreUfo(Ufo ufo)
    {
        AddScore(250);
    }

    private void ScoreAsteroid(Asteroid asteroid)
    {
        Asteroid.Size size = asteroid.size;

        switch (size)
        {
            case Asteroid.Size.Small:
                AddScore(150);
                break;
            case Asteroid.Size.Medium:
                AddScore(100);
                break;
            case Asteroid.Size.Large:
                AddScore(50);
                break;
        }
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    AddScore(-_score);
                    gameState = GameState.InProgress;
                    OnGameStart?.Invoke();
                    SetLevel(1);
                    Debug.Log("Game is started!");
                }
                break;
            case GameState.InProgress:
                //Debug.Log($"Alive enemies: {ObjectSpawner.aliveEnemies.Count}");
                if (ObjectSpawner.aliveEnemies.Count == 0)
                {
                    SetLevel(difficultyLvl+1);
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    AddScore(-_score);
                    gameState = GameState.InProgress;
                    OnGameStart?.Invoke();
                    SetLevel(1);
                }
                break;
        }
    }

    private void SetLevel(int lvl)
    {
        difficultyLvl = lvl;
        OnNextLvl?.Invoke(lvl);
    }

    public enum GameState
    {
        WaitingToStart,
        InProgress,
        //Paused?
        GameOver
    }
}
