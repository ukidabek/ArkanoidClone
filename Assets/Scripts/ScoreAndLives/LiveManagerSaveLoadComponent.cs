using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LiveManager))]
public class LiveManagerSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] private LiveManager liveManager = null;

    public override void Load(object @object)
    {
        if(@object is LiveInfo)
        {
            LiveInfo liveInfo = @object as LiveInfo;
            liveManager.LiveInfo.Lives = liveInfo.Lives;
            liveManager.UpdateLifeDisplay();
        }
    }

    public override object Save()
    {
        return liveManager.LiveInfo;
    }

    private void Reset()
    {
        liveManager = GetComponent<LiveManager>();
    }
}
