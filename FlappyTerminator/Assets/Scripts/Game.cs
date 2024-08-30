using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Pig _pig;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Score _score;
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
        Time.timeScale = 1f;
        _pig.Reset();
        _score.ResetScore();
        _enemySpawner.Reset();
        _enemySpawner.Released += _score.Add;
        _enemySpawner.gameObject.SetActive(true);
        _enemySpawner.StartGenerate();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        _enemySpawner.Released -= _score.Add;
        _enemySpawner.gameObject.SetActive(false);
        _endScreen.Open();
    }
}
