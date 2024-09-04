using System;
using UnityEngine;

public class KeyReader : MonoBehaviour
{
    private KeyCode _keySpace = KeyCode.Space;

    public event Action SpaceIsDown; 

    private void Update()
    {
        if (Input.GetKeyDown(_keySpace))
        {
            SpaceIsDown?.Invoke();
        }
    }
}
