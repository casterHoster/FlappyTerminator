using System.Collections;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    [SerializeField] private Person _owner;
    [SerializeField] private float _force;
    [SerializeField] private ProjectilePool _pool;

    private string _playerLayer = "Player";
    private string _enemyLayer = "Enemy";
    //[SerializeField] private LayerMask _enemyLayer;

    private void Start()
    {
        StartGenerate();
        _owner.Borned += StartGenerate;
    }

    public void Reset()
    {
        foreach (Projectile projectile in _pool.PooledObjects)
        {
            projectile.Collided -= PutAway;
            projectile.FlyedAway -= PutAway;
        }


        _pool.ResetPool();
    }

    protected override void Spawn()
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

    private void SetLayerMask(Projectile projectile)
    {
        if (_owner is Enemy)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer(_enemyLayer);
        }
        else if (_owner is Pig)
        {
            projectile.gameObject.layer = LayerMask.NameToLayer(_playerLayer);
        }
    }
}
