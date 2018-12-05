using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; protected set; }
    [SerializeField] private bool _dontDestroyOnLoad = false;

    protected virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = this as T;
            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarningFormat("Instance of {0} already exists. GameObject {1} will be destroyed.", typeof(T).Name, gameObject.name);
            Destroy(gameObject);
        }
    }

    protected virtual void Reset()
    {
        gameObject.name = typeof(T).Name;
    }
}
