using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _quantity;

    public Action<int> QuantityChanged;

    public void ResetScore()
    {
        _quantity = 0;
        QuantityChanged?.Invoke(_quantity);
    }

    public void Add()
    {
        _quantity++;
        QuantityChanged?.Invoke(_quantity);
    }
}
