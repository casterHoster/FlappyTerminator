using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner: MonoBehaviour
{
    [SerializeField] private float _delay;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void StartGenerate()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            Spawn();
            yield return _wait;
        }
    }

    protected abstract void Spawn();
}
