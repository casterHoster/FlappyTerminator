using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    private Queue<T> _pool;

    public IEnumerable<T> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<T>();
    }

    public T GetObjects()
    {
        if (_pool.Count == 0)
        {
            var obj = Instantiate(_prefab);

            return obj;
        }

        return _pool.Dequeue();
    }

    public void PutObject(T obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();
    }
}
