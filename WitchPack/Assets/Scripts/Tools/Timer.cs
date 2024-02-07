using System;
using UnityEngine;

public class Timer
{
    public event Action<Timer> OnTimerEnd;
    private Action _onTimerEndAction;
    private float _timer;
    private float _targetTime;
    private bool _isActive;
    private bool _usingGameTime;

    public Timer(float time, Action onTimerEnd, bool usingGameTime)
    {
        _timer = 0;
        _targetTime = time;
        _isActive = true;
        _usingGameTime = usingGameTime;
        _onTimerEndAction = onTimerEnd;
    }
    
    public void StartTimer() => _isActive = true;
    public void PauseTimer() => _isActive = false;

    public void StopTimer()
    { 
        _isActive = false;
        _timer = 0;
    } 

    public void TimerTick()
    {
        if(!_isActive) return;
        if (!_usingGameTime) _timer += Time.deltaTime;
        else _timer += GAME_TIME.GameDeltaTime;

        if (_timer >= _targetTime)
        {
            _timer = 0;
            OnTimerEnd?.Invoke(this);
            _onTimerEndAction?.Invoke();
        }
    }
}