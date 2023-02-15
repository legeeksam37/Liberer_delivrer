using TMPro;
using UnityEngine;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] TimerManager _timerManager;
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] float _redHighlightingThreshold;

    void OnEnable()
    {
        _timerManager.TimerStarted += OnTimerStarted;
    }

    void OnDisable()
    {
        _timerManager.TimerStarted -= OnTimerStarted;
    }

    void Update()
    {
        if (!_timerManager.IsActive)
            return;
        
        _text.text = $"{_timerManager.Timer:00}";
        
        if (_text.color != Color.red && _timerManager.Timer <= _redHighlightingThreshold)
            _text.color = Color.red;
    }

    void OnTimerStarted()
    {
        _text.text = $"{60}";
        _text.color = Color.white;
    }
}
