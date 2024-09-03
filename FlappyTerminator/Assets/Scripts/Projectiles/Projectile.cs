using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Projectile : MonoBehaviour, IInteractable
{
    public event Action<Projectile> FlyedAway;
    public event Action<Projectile> Collided;

    public CollisionHandler CollisionHandler;
    private float _damage = 1;

    public float Damage => _damage;

    private void Awake()
    {
        CollisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        CollisionHandler.CollisonDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        CollisionHandler.CollisonDetected += ProcessCollision;
    }

    public void ReportAboutCollided()
    {
        Collided?.Invoke(this);
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Barrier)
        {
            FlyedAway?.Invoke(this);
        }
    }
}
