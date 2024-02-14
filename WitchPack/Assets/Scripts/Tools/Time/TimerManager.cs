using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    private List<Timer> _timers = new List<Timer>();
    private List<Timer> _queuedTimersToAdd = new List<Timer>();
    private List<Timer> _queuedTimersToRemove = new List<Timer>();

    private void Update()
    {
        _timers.ForEach(timer => timer.TimerTick());

        if (_queuedTimersToAdd.Count > 0) AddQueuedTimers();
        if (_queuedTimersToRemove.Count > 0) RemoveQueuedTimers();

    }

    private void AddQueuedTimers()
    {
        _queuedTimersToAdd.ForEach(queuedTimer => _timers.Add(queuedTimer));
        _queuedTimersToAdd.Clear();
    }
    private void RemoveQueuedTimers()
    {
        _queuedTimersToRemove.ForEach(queuedTimer => _timers.Add(queuedTimer));
        _queuedTimersToAdd.Clear();
    }

    /// <summary>
    /// </summary>
    /// <param name="dontDestroyTimer">if true use the RemoveTimer Method</param>
    /// <returns></returns>
    public Timer AddTimer(float tickTime, Action onTimerEnd, bool usingGameTime = false, int ticksAmount = 1, bool dontDestroyTimer = false)
    {
        var timer = new Timer(new TimerData(tickTime, onTimerEnd,ticksAmount, usingGameTime, dontDestroyTimer));
        _queuedTimersToAdd.Add(timer);
        timer.OnTimerEnd += RemoveTimer;
        return timer;
    }
    public Timer AddTimer(Timer timer)
    {
        _queuedTimersToAdd.Add(timer);
        timer.OnTimerEnd += RemoveTimer;
        return timer;
    }

    /// <summary>
    /// use this Function if the timer is set to dont destroy
    /// </summary>
    /// <param name="timer"></param>
    public void RemoveTimer(Timer timer)
    {
        timer.OnTimerEnd -= RemoveTimer;
        _queuedTimersToRemove.Remove(timer);
    }
    
}