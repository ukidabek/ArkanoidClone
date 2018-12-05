using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiveManager : Singleton<LiveManager>
{
    [SerializeField] private LiveInfo _liveInfo = null;
    public LiveInfo LiveInfo { get { return _liveInfo; } }

    public UpdateTextEvent UpdateLiveCount = new UpdateTextEvent();
    public UnityEvent LiveLostCallback = new UnityEvent();
    public UnityEvent LiveRecoveredCallback = new UnityEvent();
    public UnityEvent EndOfLivesCallback = new UnityEvent();


    protected override void Awake()
    {
        base.Awake();
        UpdateLifeDisplay();
    }

    public void UpdateLifeDisplay()
    {
        if (_liveInfo != null)
            UpdateLiveCount.Invoke(_liveInfo.Lives.ToString());
    }

    public void AddLives(int value = 1)
    {
        if (_liveInfo != null)
        {
            _liveInfo.Lives += value;
            LiveRecoveredCallback.Invoke();
            UpdateLifeDisplay();
        }
    }

    public void RemoveLives(int value = 1)
    {
        if (_liveInfo != null)
        {
            _liveInfo.Lives -= value;
            UpdateLifeDisplay();

            if(_liveInfo.Lives > 0)
                LiveLostCallback.Invoke();
            else
                EndOfLivesCallback.Invoke();
        }
    }

    public void ResetLives()
    {
        if (_liveInfo != null)
        {
            _liveInfo.ResetLives();
            UpdateLifeDisplay();
        }
    }
}
