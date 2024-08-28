using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Projectile : MonoBehaviour, IInteractable
{
    public Action<Projectile> FlyedAway;

    public CollisionHandler CollisionHandler
    {
        get {return GetComponent<CollisionHandler>();}
    }

    private float _damage = 1;

    public Action<Projectile> Collided;

    public float Damage
    {
        get {return _damage;}
    }

    private void Awake()
    {
        CollisionHandler.CollisonDetected += ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Barrier)
        {
            FlyedAway?.Invoke(this);
        }
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
