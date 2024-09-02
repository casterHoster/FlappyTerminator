using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    private Queue<T> ActiveObjects;
    private List<T> _allCreatedObjects;

    public IEnumerable<T> PooledObjects => _allCreatedObjects;

    private void Awake()
    {
        ActiveObjects = new Queue<T>();
        _allCreatedObjects = new List<T>();
    }

    public T GetObject()
    {
        if (ActiveObjects.Count == 0)
        {
            T obj = Instantiate(_prefab);
            _allCreatedObjects.Add(obj);
            return obj;
        }

        return ActiveObjects.Dequeue();
    }

    public void PutObject(T obj)
    {
        obj.gameObject.SetActive(false);
        ActiveObjects.Enqueue(obj);
    }

    public void ResetPool()
    {
        foreach (var obj in _allCreatedObjects)
        {
            obj.gameObject.SetActive(false);
        }

        ActiveObjects = new Queue<T>(_allCreatedObjects);
    }
}
