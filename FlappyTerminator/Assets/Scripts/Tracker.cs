using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Pig _pig;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        Vector3 position = transform.position;
        position.x = _pig.transform.position.x + _xOffset;
        transform.position = position;
    }
}
