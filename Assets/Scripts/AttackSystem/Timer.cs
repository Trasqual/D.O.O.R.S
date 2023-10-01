using System;
using UnityEngine;

public class Timer
{
    private Action _onTimeUp;
    private float _cooldown;
    private float _timer;
    private bool _isActive;

    public void Init(float cooldown, Action OnTimeUp)
    {
        _cooldown = cooldown;
        _onTimeUp = OnTimeUp;
    }

    public void UpdateTimer()
    {
        if (_isActive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _onTimeUp?.Invoke();
                _timer = _cooldown;
            }
        }
    }

    public void StartTimer()
    {
        _isActive = true;
    }

    public void StopTimer()
    {
        _isActive = false;

    }
}
