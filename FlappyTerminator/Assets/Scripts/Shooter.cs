using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Person _owner;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _delay;
    [SerializeField] private float _force;
    [SerializeField] private ProjectilePool _pool; 

    private WaitForSeconds _wait;
    private List<Projectile> _projectileList;

    private void Start()
    {
        _projectileList = new List<Projectile>();
        _owner.Reseted += Reset;
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
        foreach (Projectile projectile in _projectileList)
        {
            _pool.PutObject(projectile);
        }

        _projectileList.Clear();
        _pool.Reset();
    }

    private void Initialize()
    {
        Projectile projectile = _pool.GetObjects();
        _projectileList.Add(projectile);
        projectile.TimeIsOver += PutAway;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = _owner.transform.rotation;
        projectile = SetLayerMask(projectile);
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
        _projectileList.Remove(projectile);
        _pool.PutObject(projectile);
    }

    private IEnumerator Fire()
    {
        while (enabled)
        {
            Initialize();
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
