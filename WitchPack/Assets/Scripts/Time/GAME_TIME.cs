using System;
using System.Collections;
using UnityEngine;


/// <summary>
/// GAME_TIME is a static class that manages the game time and time manipulation.
/// </summary>
public class GAME_TIME : MonoBehaviour
{
    public static event Action<float> OnTimeRateChange;
    private static float _gameTime;
    private static float _timeRate = 1f;
    private static float _startGameTime;
    private static AnimationCurve _defaultCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private static float _tempTimeData = 1;
    public static float TimePlayed => Time.realtimeSinceStartup - _startGameTime;
    public static float GameTime => _gameTime;
    public static float TimeRate => _timeRate;
    public static float GameDeltaTime => Time.deltaTime * _timeRate;
    public static float GameFixedDeltaTime => Time.fixedDeltaTime * _timeRate;
    public static bool IsTimeStopped => _timeRate == 0;
    private static MonoBehaviour _monoBehaviour;
    private static Coroutine _fadeCoroutine;
    private static float _transitionTimeCount = 0;

    private void Awake()
    {
        _monoBehaviour = this;
        _startGameTime = Time.realtimeSinceStartup;
        _gameTime = 0;
    }
    private void Update()
    {
        _gameTime += GameDeltaTime;
    }

    public static void StartGame()
    {
        _startGameTime = Time.time;
    }

    /// <summary>
    /// Sets the time step for the game time.
    /// </summary>
    /// <param name="time">The new time step to set.</param>
    /// <param name="transitionTime">The amount of time to transition to the new time step.</param>
    /// <param name="curve">The animation curve to use for the time transition effect.</param>
    public static void SetTimeStep(float time, float transitionTime = 1, AnimationCurve curve = null)
    {
        if (time < 0)
        {
            Debug.LogError("Can not set timeStep to less or equal to 0");
            return;
        }

        if (_fadeCoroutine != null)
        {
            _monoBehaviour.StopCoroutine(_fadeCoroutine);
            SetTime(1f);
            _fadeCoroutine = null;
        }

        if (curve == null)
            SetTime(time);
        else
            _fadeCoroutine = _monoBehaviour.StartCoroutine(FadeTime(time, transitionTime, curve));
    }

    /// <summary>
    /// FadeTime method is used to set the time step of the game, which controls the rate at which time progresses.
    /// It allows for a smooth transition from the current time step to the desired time step over a specified transition time, using an optional animation curve.
    /// </summary>
    /// <param name="time">The desired time step value to set. Must be greater than 0.</param>
    /// <param name="transitionTime">The duration of the transition from the current time step to the desired time step. Default value is 1.</param>
    /// <param name="curve">Optional animation curve to define the transition curve. If not provided, a default linear curve will be used.</param>
    private static IEnumerator FadeTime(float time, float transitionTime = 1, AnimationCurve curve = null)
    {
        float currentTimeRate = _timeRate;

        var animationCurve = curve ?? _defaultCurve;

        while (_transitionTimeCount < transitionTime)
        {
            _transitionTimeCount += Time.deltaTime;

            float evaluateValue = animationCurve.Evaluate(_transitionTimeCount / transitionTime);

            SetTime(Mathf.Lerp(currentTimeRate, time, evaluateValue));

            yield return null;
        }

        _transitionTimeCount = 0;
        SetTime(time);
    }

    /// <summary>
    /// Sets the game time step to the specified value over a transition period.
    /// </summary>
    /// <param name="time">The target time step value.</param>
    /// <param name="transitionTime">The duration of the transition in seconds (default is 1 second).</param>
    /// <param name="curve">The animation curve to control the speed of the transition (default is a linear curve).</param>
    private static void SetTime(float timeRate)
    {
        _timeRate = timeRate;

        //Debug.Log($"Set time to {timeRate}");

        OnTimeRateChange?.Invoke(_timeRate);
    }

    /// <summary>
    /// Method to resume gameplay after pausing the game.
    /// </summary>
    public static void Play()
    {
        SetTimeStep(_tempTimeData);

        //Debug.Log($"<color={ColorLogHelper.GREEN}>PLAY</color>");
        _tempTimeData = 0;
    }

    /// <summary>
    /// Pauses the game time.
    /// </summary>
    /// <remarks>
    /// This method is used to pause the game time by setting the time step to 0.
    /// It also logs a message indicating that the game is paused.
    /// </remarks>
    public static void Pause()
    {
        if (_timeRate == 0) return;
        _tempTimeData = _timeRate;

        //Debug.Log($"<color={ColorLogHelper.RED}>PAUSE</color>");
        SetTimeStep(0);
    }
    
    
}