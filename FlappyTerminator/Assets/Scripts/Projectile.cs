using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IInteractable
{
    private float _damage = 1;
    private float _lifeTime = 3;

    public Action <Projectile> TimeIsOver;

    public float GetDamage()
    {
        return _damage;
    }

    private void OnEnable()
    {
        StartCoroutine(CountLifeTime());
    }

    private IEnumerator CountLifeTime()
    {
        WaitForSeconds delay = new WaitForSeconds(_lifeTime);
        yield return delay;
        TimeIsOver?.Invoke(this);
    }
}
