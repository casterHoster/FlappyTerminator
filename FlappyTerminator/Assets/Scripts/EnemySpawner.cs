using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private EnemyPool _pool;

    private WaitForSeconds _wait;
    private List<Enemy> _enemyList;

    public Action Reseted;
    public Action PutAwayed;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _enemyList = new List<Enemy>();
    }

    public void StartGenerate()
    {
        StartCoroutine(Generate());
    }

    public void Reset()
    {
        foreach (Enemy enemy in _enemyList)
        {
            _pool.PutObject(enemy);
        }

        _enemyList.Clear();
        _pool.Reset();
    }

    private void Initialize()
    {
        float spawnPositionY = UnityEngine.Random.Range(_upperBound, _lowerBound);
        Enemy enemy = _pool.GetObjects();
        _enemyList.Add(enemy);
        enemy.ResetHealth();
        enemy.transform.position = new Vector3(_rightBorder.transform.position.x, spawnPositionY, _rightBorder.transform.position.z);
        enemy.transform.rotation = _enemy.transform.rotation;
        enemy.Died += PutAway;
        enemy.gameObject.SetActive(true);
    }

    private void PutAway(Enemy enemy)
    {
        enemy.Died -= PutAway;
        _enemyList.Remove(enemy);
        _pool.PutObject(enemy);
        PutAwayed?.Invoke();
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            Initialize();
            yield return _wait;
        }
    }
}
