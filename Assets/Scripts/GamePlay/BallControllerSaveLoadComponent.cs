using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallController))]
public class BallControllerSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] private BallController _ballController = null;

    public override void Load(object @object)
    {
        if(@object is BallControllerStatus)
        {
            BallControllerStatus status = @object as BallControllerStatus;
            _ballController.BallControllerStatus.CurrentPosition = status.CurrentPosition;
            _ballController.BallControllerStatus.CurrentVelocity = status.CurrentVelocity;
            _ballController.BallControllerStatus.CurrentLevel = status.CurrentLevel;
        }
    }

    public override object Save()
    {
        return _ballController.BallControllerStatus;
    }

    private void Reset()
    {
        _ballController = GetComponent<BallController>();
    }
}
