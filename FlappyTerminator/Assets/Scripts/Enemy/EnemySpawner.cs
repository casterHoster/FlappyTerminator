using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private EnemyPool _pool;

    public event Action Released;

    public void Reset()
    {
        foreach (Enemy enemy in _pool.PooledObjects)
        {
            enemy.Died -= PutAway;
            enemy.ResetProjectiles();
        }

        _pool.ResetPool();
    }

    protected override void Spawn()
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

        if (enemy.IsGivePoints)
        {
        Released?.Invoke();
        }
    }
}
