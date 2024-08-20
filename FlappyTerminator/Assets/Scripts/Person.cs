using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Person : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    protected float Health;
    private CollisionHandler _collisionHandler;

    public Action Borned;
    public Action Reseted;

    protected virtual void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
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
