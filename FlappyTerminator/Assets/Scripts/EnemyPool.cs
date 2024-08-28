using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    public void Reset()
    {
        foreach (Enemy obj in _pool)
        {
            obj.Reseted?.Invoke();
        }
    }

    public void PutObject(Enemy obj)
    {
        Debug.Log("in pool");
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
