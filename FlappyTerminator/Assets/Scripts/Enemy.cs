using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : Person, IInteractable
{
    private float _damage = 1;

    public event Action<Enemy> Died;

    public float GetDamage()
    {
        return _damage;
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }

    protected override void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Projectile)
        {
            Health--;
        }

        if (Health <= 0)
        {
            Die();
        }

        if (interactable is Barrier)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Died?.Invoke(this);
    }
}
