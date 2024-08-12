using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent (typeof(Score))]
[RequireComponent (typeof(CollisionHandler))]
public class Pig : Person
{
    private Mover _mover;
    private Score _score;

    public Action<Pig> Died;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
        _score = GetComponent<Score>();
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
            Health -= projectile.GetDamage();
        }

        if (Health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Time.timeScale = 0;
        Died?.Invoke(this);
    }

    public void Reset()
    {
        _mover.Reset();
    }
}
