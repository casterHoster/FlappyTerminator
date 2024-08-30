using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private EnemyPool _pool;

    private WaitForSeconds _wait;

    public Action Reseted;
    public event Action Released;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void StartGenerate()
    {
        StartCoroutine(Generate());
    }

    public void Reset()
    {
        foreach (Enemy enemy in _pool.PooledObjects)
        {
            enemy.Died -= PutAway;
            enemy.ResetProjectiles();
        }

        _pool.Reset();
    }

    private void Spawn()
    {
        float spawnPositionY = UnityEngine.Random.Range(_upperBound, _lowerBound);
        Enemy enemy = _pool.GetObject();
        enemy.transform.position = new Vector3(_rightBorder.transform.position.x, spawnPositionY, _rightBorder.transform.position.z);
        enemy.Died += PutAway;
        enemy.gameObject.SetActive(true);
    }

    private void PutAway(Enemy enemy)
    {
        enemy.Died -= PutAway;
        enemy.ResetHealth();
        _pool.PutObject(enemy);
        Released?.Invoke();
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            yield return _wait;
            Spawn();
        }
    }
}
