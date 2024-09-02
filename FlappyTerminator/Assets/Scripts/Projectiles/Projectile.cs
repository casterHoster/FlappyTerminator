using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Projectile : MonoBehaviour, IInteractable
{
    public Action<Projectile> FlyedAway;

    private float _damage = 1;

    public Action<Projectile> Collided;

    public CollisionHandler CollisionHandler
    {
        get {return GetComponent<CollisionHandler>();}
    }

    private void Awake()
    {
        CollisionHandler.CollisonDetected += ProcessCollision;
    }

    public float Damage
    {
        get {return _damage;}
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Barrier)
        {
            FlyedAway?.Invoke(this);
        }
    }
}
