using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile _prefab;

    private Queue<Projectile> _pool;

    public IEnumerable<Projectile> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Projectile>();
    }

    public Projectile GetObjects()
    {
        if (_pool.Count == 0)
        {
            var obj = Instantiate(_prefab);
            _pool.Enqueue(obj);
        }

        return _pool.Dequeue();
    }

    public void PutObject(Projectile obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (Projectile obj in _pool)
        {
            obj.DestroyGameobject();
        }

        _pool.Clear();
    }
}
