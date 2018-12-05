using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTimeAction : BaseAction
{
    [SerializeField] private float _timeScale = .5f;
    [SerializeField] private float _duration = 3f;

    public override void Perform(GameObject gameObject, Collision2D collision)
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.QueueSlowdown(_timeScale, _duration);
    }
}
