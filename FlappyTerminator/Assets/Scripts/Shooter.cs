using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Person _owner;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _delay;
    [SerializeField] private float _force;

    private ObjectPool<Projectile> _pool; 
    private WaitForSeconds _wait;

    private void OnEnable()
    {
        _owner.Borned += StartFire;
    }

    private void Start()
    {
        StartFire();
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _pool = new ObjectPool<Projectile>(
            createFunc: Create,
            actionOnGet: (projectile) => Initialize(projectile),
            actionOnRelease: (projectile) => Disable(projectile));
    }

    private void StartFire()
    {
        StartCoroutine(Fire());
    }

    private Projectile Create()
    {
        Projectile projectile = Instantiate(_projectile, transform.position, _owner.transform.rotation);

        return SetLayerMask(projectile);
    }

    private void Initialize(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.TimeIsOver += PutAway;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = _owner.transform.rotation;
        projectile = SetLayerMask(projectile);

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
        _pool.Release(projectile);
    }

    private void Disable(Projectile projectile)
    {
        projectile.TimeIsOver -= PutAway;
        projectile.gameObject.SetActive(false);
    }

    private IEnumerator Fire()
    {
        while (enabled)
        {
            _pool.Get();

            yield return _wait;
        }
    }

    private Projectile SetLayerMask(Projectile projectile)
    {
        if (_owner is Enemy)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        else

        if (_owner is Pig)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        return projectile;
    }
}
