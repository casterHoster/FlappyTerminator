using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Person _owner;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _delay;
    [SerializeField] private float _force;
    [SerializeField] private ProjectilePool _pool; 

    private WaitForSeconds _wait;
    private string _playerLayer = "Player";
    private string _enemyLayer = "Enemy";


    private void Start()
    {
        StartFire();
        _owner.Borned += StartFire;
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void StartFire()
    {
        StartCoroutine(Fire());
    }

    public void Reset()
    {
        foreach (Projectile projectile in _pool.PooledObjects)
        {
            projectile.Collided -= PutAway;
            projectile.FlyedAway -= PutAway;
        }

        
        _pool.Reset();
    }

    private void Spawn()
    {
        Projectile projectile = _pool.GetObject();
        projectile.Collided += PutAway;
        projectile.FlyedAway += PutAway;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = _owner.transform.rotation;
        SetLayerMask(projectile);
        projectile.gameObject.SetActive(true);

        if (projectile.TryGetComponent(out Rigidbody2D rigidbody2d)) 
          {
            if (transform.rotation.y >= 0)
            {
                rigidbody2d.velocity = new Vector3(_force, _owner.transform.rotation.z * _force);
            }
            else
            {
                rigidbody2d.velocity = new Vector3(-1 * _force, _owner.transform.rotation.z * _force);
            }
          }
    }

    private void PutAway(Projectile projectile)
    {
        _pool.PutObject(projectile);
        projectile.Collided -= PutAway;
        projectile.FlyedAway -= PutAway;
    }

    private IEnumerator Fire()
    {
        while (enabled)
        {
            Spawn();
            yield return _wait;
        }
    }

    private void SetLayerMask(Projectile projectile)
    {
        if (_owner is Enemy)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer(_enemyLayer);
        }
        else

        if (_owner is Pig)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer(_playerLayer);
        }
    }
}
