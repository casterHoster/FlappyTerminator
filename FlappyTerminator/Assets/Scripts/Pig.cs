using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent (typeof(CollisionHandler))]
public class Pig : Person
{
    [SerializeField] private Vector3 _startPosition;

    private Mover _mover;
    public Action Died;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _mover.Reset();
    }

    protected override void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Barrier)
        {
            Die();
        }

        if (interactable is Enemy)
        {
            Enemy enemy = (Enemy) interactable;
            Health -= enemy.GetDamage();
        }

        if (interactable is Projectile)
        {
            Projectile projectile = (Projectile) interactable;
            Health -= projectile.GiveDamage();
        }

        if (Health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Died?.Invoke();
    }
}
