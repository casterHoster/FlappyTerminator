using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    protected Queue<T> _pool;

    public IEnumerable<T> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<T>();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            return Instantiate(_prefab);
        }

        return _pool.Dequeue();
    }

    public void PutObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
