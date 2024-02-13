using System;
using UnityEngine;

public class Timer
{
    public event Action<Timer> OnTimerEnd;
    
    private readonly Action _onTimerTick;
    private readonly float _tickTime;
    private readonly float _tickAmount;
    private readonly bool _usingGameTime;
    private readonly bool _dontDestroyTimer;
    
    private float _timer;
    private int _currentTicks;
    private bool _isActive;

    public Timer(float tickTime, int tickAmount, Action onTimerEnd, bool usingGameTime,bool dontDestroyTimer)
    {
        _timer = 0;
        _tickTime = tickTime;
        _tickAmount = tickAmount; 
        _isActive = true;
        _usingGameTime = usingGameTime;
        _dontDestroyTimer = dontDestroyTimer;
        _onTimerTick = onTimerEnd;
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
        
        //update timer
        if (!_usingGameTime) _timer += Time.deltaTime;
        else _timer += GAME_TIME.GameDeltaTime;
        
        if (_timer >= _tickTime)
        {
            _timer = 0;
            _onTimerTick?.Invoke();
            
            if(_dontDestroyTimer) return;
            
            _currentTicks++;
            if (_currentTicks >= _tickAmount)
            {
                _isActive = false;
                OnTimerEnd?.Invoke(this);
            }
        }
    }
}