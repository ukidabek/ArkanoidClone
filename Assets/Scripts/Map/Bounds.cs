using System;
using UnityEngine;

public partial class MapGenerator
{
    [Serializable] public class Bounds
    {
        public Vector3 Position = Vector3.zero;
        public Vector2 Size = Vector2.zero;
        public Vector2 Dimensions = Vector3.zero;

        public float MinX { get { return Size.x; } }
        public float MaxX { get { return -Size.x; } }
        public float MinY { get { return -Size.y; } }
        public float MaxY { get { return Size.y; } }

        public Bounds() {}
        public Bounds (Vector3 position, Vector2 size)
        {
            Position = position;
            Size = size;
            Dimensions = size * 2;
        }
    }
}

