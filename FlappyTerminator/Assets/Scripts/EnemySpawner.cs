using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;

    private ObjectPool<Enemy> _pool;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _pool = new ObjectPool<Enemy>(
            createFunc: Create,
            actionOnGet: (enemy) => Initialize(enemy),
            actionOnRelease: (enemy) => Disable(enemy));
    }

    public void StartGenerate()
    {
        StartCoroutine(Generate());
    }

    private Enemy Create()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        return Instantiate(_enemy, new Vector3(_rightBorder.transform.position.x, 
            spawnPositionY, _rightBorder.transform.position.z), _enemy.transform.rotation);
    }

    private void Initialize(Enemy enemy)
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        enemy.transform.position = new Vector3(_rightBorder.transform.position.x,
            spawnPositionY, _rightBorder.transform.position.z);
        enemy.Died += PutAway;
        enemy.ResetHealth();
        enemy.gameObject.SetActive(true);
    }

    private void Disable(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void PutAway(Enemy enemy)
    {
        enemy.Died -= PutAway;
        _pool.Release(enemy);
    }

    private IEnumerator Generate()
    {
        while(Time.timeScale != 0)
        {
            _pool.Get();
            yield return _wait;
        }
    }

    public void Reset()
    {
        
    }
}
