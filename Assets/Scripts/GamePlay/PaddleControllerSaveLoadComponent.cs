using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleControllerSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] private PaddleController _paddleController = null;

    public override void Load(object @object)
    {
        if(@object is PaddleControllerStatus)
        {
            PaddleControllerStatus paddleControllerStatus = @object as PaddleControllerStatus;
            _paddleController.PaddleControllerStatus.CurrentPosition = paddleControllerStatus.CurrentPosition;
        }
    }

    public override object Save()
    {
        return _paddleController.PaddleControllerStatus;
    }

    private void Reset()
    {
        _paddleController = GetComponent<PaddleController>();
    }
}
