using UnityEngine;
using System;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Person : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private ProjectileSpawner _projectileSpawner;

    protected float Health;
    private CollisionHandler _collisionHandler;

    public event Action Borned;

    protected virtual void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    public void ResetProjectiles()
    {
        _projectileSpawner.Reset();
    }

    public void ResetHealth()
    {
        Health = _maxHealth;
    }

    protected virtual void OnEnable()
    {
        _collisionHandler.CollisonDetected += ProcessCollision;
        Borned?.Invoke();
    }

    protected abstract void ProcessCollision(IInteractable interactable);

    protected abstract void Die();
}
