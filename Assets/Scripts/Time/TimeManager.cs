using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Time = UnityEngine.Time;

/// <summary>
/// Time manager.
/// The purpose of this class is a time management.
/// </summary>
public class TimeManager : Singleton<TimeManager>
{
    private const float _maximumDeltaTimeFactor = 3;

    private float _defaultTimeScale = 0f;
    private float _defaultFixedDeltaTime = 0f;

    private float _onPouseTimeScale = 0f;
    private float _onPouseFixedDeltaTime = 0f;

    public EnableTimeDisplayEvent EnableTimeDisplayCallback = new EnableTimeDisplayEvent();
    public UpdateTimeDisplayEvent UpdateTimeDisplayCallback = new UpdateTimeDisplayEvent();

    [Serializable] public class SlowDown
    {
        private float _initialDuration = 0; 
        public float Duration = 0;
        public float DurationProcentage { get { return Duration / _initialDuration; } }

        public float TimeScale = 1f;

        public SlowDown(float duration, float timeScale)
        {
            SetDuration(duration);
            TimeScale = timeScale;
        }

        public void SetDuration(float duration)
        {
            _initialDuration = Duration = duration;
        }
    }

    private SlowDown _currentSlowdown = null;
    				
    protected void CacheDefaultTimeValues()
    {
        _onPouseTimeScale = _defaultTimeScale = Time.timeScale;
        _onPouseFixedDeltaTime = _defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    internal void QueueSlowdown(float timeScale, float duration)
    {
        if (_currentSlowdown != null)
        {
            _currentSlowdown.TimeScale = timeScale;
            _currentSlowdown.SetDuration(duration);
        }
        else
            _currentSlowdown = new SlowDown(duration, timeScale);

        SetNewTimeScale(_currentSlowdown.TimeScale);
        EnableTimeDisplayCallback.Invoke(true);
        UpdateTimeDisplayCallback.Invoke(_currentSlowdown.DurationProcentage);
    }

    public void RestTimeScale()
    {
        _onPouseTimeScale = Time.timeScale = _defaultTimeScale;
        _onPouseFixedDeltaTime = Time.fixedDeltaTime = _defaultFixedDeltaTime;
#if UNITY_EDITOR
        Debug.LogFormat("Time scale is set to: {0}", Time.timeScale);
#endif
    }


    public void SetNewTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
#if UNITY_EDITOR
        Debug.LogFormat("Time scale is set to: {0}", Time.timeScale);
#endif
    }

    protected override void Awake()
    {
        base.Awake();
        CacheDefaultTimeValues();
        EnableTimeDisplayCallback.Invoke(false);
    }

    public void Update()
    {
        if(_currentSlowdown != null)
        {
            _currentSlowdown.Duration -= Time.deltaTime * (1 / (1 - Time.timeScale));
            UpdateTimeDisplayCallback.Invoke(_currentSlowdown.DurationProcentage);
            if(_currentSlowdown.Duration < 0)
            {
                EnableTimeDisplayCallback.Invoke(false);
                RestTimeScale();
                _currentSlowdown = null;
            }
        }

    }

    public void Pause()
    {
        _onPouseTimeScale = Time.timeScale;
        _onPouseFixedDeltaTime = Time.fixedDeltaTime;

        SetNewTimeScale(0);
        gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = _onPouseTimeScale;
        Time.fixedDeltaTime = _onPouseFixedDeltaTime;

#if UNITY_EDITOR
        Debug.LogFormat("Time scale is set to: {0}", Time.timeScale);
#endif

        gameObject.SetActive(true);
    }

    public void ClearQueue()
    {
        _currentSlowdown = null;
        EnableTimeDisplayCallback.Invoke(false);
        RestTimeScale();
    }
}
