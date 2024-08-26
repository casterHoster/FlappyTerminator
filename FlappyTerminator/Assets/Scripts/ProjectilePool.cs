using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : ObjectPool<Projectile>
{
    public override void Reset()
    {
        foreach (Projectile obj in _pool)
        {
            obj.DestroyGameobject();
        }

        base.Reset();
    }
}
