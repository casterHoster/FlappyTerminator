using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;

    private Queue<Enemy> _pool;

    public IEnumerable<Enemy> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Enemy>();
    }

    public Enemy GetObjects()
    {
        if (_pool.Count == 0)
        {
            var obj = Instantiate(_prefab);
            _pool.Enqueue(obj);
        }

        return _pool.Dequeue();
    }

    public void PutObject(Enemy obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (Enemy obj in _pool)
        {
            obj.Reseted?.Invoke();
            obj.DestroyGameobject();
        }

        _pool.Clear();
    }
}
