using System.Collections;
using UnityEngine;

public abstract class Spawner: MonoBehaviour
{
    [SerializeField] private float _delay;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    protected abstract void Spawn();

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
}
