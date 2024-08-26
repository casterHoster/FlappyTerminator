using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IInteractable
{
    private float _damage = 1;
    private float _lifeTime = 3;

    public float Damage
    {
        get {return _damage;}
    }

    public Action <Projectile> TimeIsOver;

    private void OnEnable()
    {
        StartCoroutine(CountLifeTime());
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }

    private IEnumerator CountLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        TimeIsOver?.Invoke(this);
    }
}
