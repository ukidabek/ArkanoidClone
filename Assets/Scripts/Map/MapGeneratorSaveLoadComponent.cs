using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapGeneratorSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] MapGenerator _mapGenerator = null;
    public override void Load(object @object)
    {
        if(@object is MapStatus)
        {
            MapStatus mapStatus = @object as MapStatus;
            _mapGenerator.MapStatus.Seed = mapStatus.Seed;
            _mapGenerator.MapStatus.BlockStatus = mapStatus.BlockStatus;
        }
    }

    public override object Save()
    {
        _mapGenerator.SaveMapStatus();
        return _mapGenerator.MapStatus;
    }

    private void Reset()
    {
        _mapGenerator = GetComponent<MapGenerator>();
    }
}
