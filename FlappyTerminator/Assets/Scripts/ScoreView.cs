using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _score.QuantityChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _score.QuantityChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _text.text = score.ToString();
    }
}
