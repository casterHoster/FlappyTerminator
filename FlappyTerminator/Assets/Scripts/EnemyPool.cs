using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    public override void Reset()
    {
        foreach (Enemy obj in _pool)
        {
            obj.Reseted?.Invoke();
        }

        //base.Reset();
    }
}
