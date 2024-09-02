using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    protected Queue<T> _pool;
    private List<T> _allPull;

    public IEnumerable<T> PooledObjects => _allPull;

    private void Awake()
    {
        _pool = new Queue<T>();
        _allPull = new List<T>();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T obj = Instantiate(_prefab);
            _allPull.Add(obj);
            return obj;
        }

        return _pool.Dequeue();
    }

    public void PutObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void ResetPool()
    {
        foreach (var obj in _allPull)
        {
            obj.gameObject.SetActive(false);
        }

        _pool = new Queue<T>(_allPull);
    }
}
