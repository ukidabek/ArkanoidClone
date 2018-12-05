using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MapStatus.asset", menuName = "MapStatus")]
[Serializable] public class MapStatus //: ScriptableObject
{
    [SerializeField] private int _minSeed = 0;
    [SerializeField] private int _maxSeed = 10000;

    public int Seed = 0;

    public List<bool> BlockStatus = new List<bool>();

    public void GenerateSeed()
    {
        Seed = UnityEngine.Random.Range(_minSeed, _maxSeed);
    }
}
