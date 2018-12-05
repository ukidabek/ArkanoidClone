using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BallControllerStatus.asset", menuName = "BallControllerStatus")]
[Serializable] public class BallControllerStatus// : ScriptableObject
{
    [SerializeField] private int _currentLevel = 1;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public Vector3Serializable CurrentVelocity = new Vector3Serializable();
    public Vector3Serializable CurrentPosition = new Vector3Serializable();

    public void ResetStatus()
    {
        _currentLevel = 1;
    }

    public void GoToNextLevel()
    {
        ++_currentLevel;
    }
}


[Serializable] public class Vector2Serializable
{
    public float x = 0;
    public float y = 0;

    public void Serialize(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }

    public static implicit operator Vector2(Vector2Serializable vector2Serializable)
    {
        return new Vector2(vector2Serializable.x, vector2Serializable.y);
    }
}

[Serializable] public class Vector3Serializable : Vector2Serializable
{
    public float z = 0;

    public void Serialize(Vector3 vector)
    {
        z = vector.z;
        base.Serialize(vector);
    }

    public static implicit operator Vector3 (Vector3Serializable vector3Serializable)
    {
        return new Vector3(vector3Serializable.x, vector3Serializable.y, vector3Serializable.z);
    }
}