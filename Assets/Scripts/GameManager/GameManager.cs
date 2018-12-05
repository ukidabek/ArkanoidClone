using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [Serializable] public class GameMode
    {
        [SerializeField] private string _mode = string.Empty;
        public string Mode { get { return _mode; } }

        public UnityEvent OnGameModeActivation = new UnityEvent();
        public UnityEvent OnGameModeDeactivation = new UnityEvent();

        [SerializeField] private bool _toggable = false;
        public bool Toggable { get { return _toggable; } }

        [SerializeField] private string _returnToModeName = string.Empty;
        public string ReturnToModeName { get { return _returnToModeName; } }

        [SerializeField] List<string> _requiredStateName = new List<string>();
        public List<string> RequiredStateName { get { return _requiredStateName; } }
    }

    [SerializeField] private string _mainMenuModeName = "MainMenu";
    [SerializeField] private GameMode _currentMode = null;
    [SerializeField] private List<GameMode> _gameModes = new List<GameMode>();

    protected void Start()
    {
        SetGameMode(_mainMenuModeName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public GameMode GetGameMode(string modeName)
    {
        foreach (var item in _gameModes)
        {
            if (item.Mode == modeName)
                return item;
        }

        return null;
    }

    public void SetGameMode(string mode)
    {
        GameMode gameMode = GetGameMode(mode);
        if (gameMode == null)
            return;

        if (gameMode.RequiredStateName.Count > 0)
            if (!gameMode.RequiredStateName.Contains(_currentMode.Mode))
                return;

        if (_currentMode != null)
        {
            if (_currentMode.Mode == mode)
            {
                if (_currentMode.Toggable && !string.IsNullOrEmpty(_currentMode.ReturnToModeName))
                    SetGameMode(_currentMode.ReturnToModeName);

                return;
            }
            else
                _currentMode.OnGameModeDeactivation.Invoke();
        }

        _currentMode = gameMode;
        _currentMode.OnGameModeActivation.Invoke();
    }
}
