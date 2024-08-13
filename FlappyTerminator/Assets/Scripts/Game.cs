using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Pig _pig;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endScreen;

    private void Start()
    {
        Time.timeScale = 0f;
        _startScreen.Open();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endScreen.RestartButtonClicked += OnPlayButtonClick;
        _pig.Died += StopGame;
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _enemySpawner.StartGenerate();
        _pig.Reset();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        _endScreen.Open();
    }
}
