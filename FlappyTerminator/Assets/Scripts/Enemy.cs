using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : Person, IInteractable
{
    private float _damage = 1;
    private bool _isGivePoints;

    public event Action<Enemy> Died;

    public bool IsGivePoints => _isGivePoints;

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
            Projectile projectile = (Projectile)interactable;
            projectile.Collided?.Invoke(projectile);

            if (Health <= 0)
            {
                _isGivePoints = true;
                Die();
            }
        }

        if (interactable is Barrier)
        {
            _isGivePoints = false;
            Die();
        }
    }

    protected override void Die()
    {
         Died?.Invoke(this);
    }
}
