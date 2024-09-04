using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(KeyReader))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private KeyReader _keyReader;

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _keyReader = GetComponent<KeyReader>();
        _maxRotation = Quaternion.Euler(0,0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        _keyReader.SpaceIsDown += Jump;
    }

    private void OnDestroy()
    {
        _keyReader.SpaceIsDown -= Jump;
    }

    private void Update()
    { 
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = new Vector2();
        transform.rotation = new Quaternion();
    }
}
