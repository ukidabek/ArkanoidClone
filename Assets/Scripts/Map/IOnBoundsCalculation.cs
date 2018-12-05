using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnBoundsCalculation
{
    void UpdateBounds(Vector3 position, Vector2 size);
}
