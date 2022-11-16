using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState gameState;
    public static GameManager gameManager;

    public event Action OnGameStart; 
    public event Action OnGameOver;

    private void Awake()
    {
        gameManager = this;
        gameState = GameState.WaitingToStart;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameState = GameState.InProgress;
        OnGameStart?.Invoke();
        Debug.Log("Game is started!");
    }

    public enum GameState
    {
        WaitingToStart,
        InProgress,
        //Paused?
        GameOver
    }
}
