using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IInteractable
{
    private float _damage = 1;
    private float _lifeTime = 3;

    public Action <Projectile> TimeIsOver;

    private void OnEnable()
    {
        StartCoroutine(CountLifeTime());
    }

    public float GiveDamage()
    {
        return _damage;
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }

    private IEnumerator CountLifeTime()
    {
        WaitForSeconds delay = new WaitForSeconds(_lifeTime);
        yield return delay;
        TimeIsOver?.Invoke(this);
    }
}
