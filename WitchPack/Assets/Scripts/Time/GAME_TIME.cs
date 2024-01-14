using System;
using System.Collections;
using UnityEngine;


public class GAME_TIME : MonoBehaviour
{
    public static event Action OnTimeRateChange;

    private static float _gameTime;
    private static float _timeRate = 1f;
    private static float _startGameTime;

    private static AnimationCurve _defaultCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private static float _tempTimeData = 1;
    public static float TimePlayed => Time.realtimeSinceStartup - _startGameTime;
    public static float GetCurrentTimeRate => _timeRate;
    public static float GameTime => _gameTime;
    public static float GameDeltaTime => Time.deltaTime * _timeRate;

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

    private static IEnumerator FadeTime(float time, float transitionTime = 1, AnimationCurve curve = null)
    {
        float currentTimeRate = GetCurrentTimeRate;

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

    private static void SetTime(float timeRate)
    {
        _timeRate = timeRate;

        //Debug.Log($"Set time to {timeRate}");

        OnTimeRateChange?.Invoke();
    }

    public static void Play()
    {
        SetTimeStep(_tempTimeData);

        Debug.Log($"<color={ColorLogHelper.GREEN}>PLAY</color>");
        _tempTimeData = 0;
    }

    public static void Pause()
    {
        if (_timeRate == 0) return;
        _tempTimeData = _timeRate;

        Debug.Log($"<color={ColorLogHelper.RED}>PLAY</color>");
        SetTimeStep(0);
    }
}