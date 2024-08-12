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

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endScreen.PlayButtonClicked += OnPlayButtonClick;
    }

    private void OnPlayButtonClick()
    {
        throw new NotImplementedException();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _pig.Reset();
    }
}
