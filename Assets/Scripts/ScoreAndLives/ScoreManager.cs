using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private ScoreInfo _scoreInfo = null;
    public ScoreInfo ScoreInfo { get { return _scoreInfo; } }

    [SerializeField] private LeaderBoard _leaderBoard = new LeaderBoard();
    public LeaderBoard LeaderBoard { get { return _leaderBoard; } }

    public UpdateTextEvent UpdateScoreCallback = new UpdateTextEvent();
    public UpdateTextEvent UpdateNameCallback = new UpdateTextEvent();


    protected override void Awake()
    {
        base.Awake();
        UpdateScoreDisplay();
    }

    public void UpdateScore(int score)
    {
        if (_scoreInfo != null)
        {
            _scoreInfo.CurrentScore += score;
            UpdateScoreDisplay();
        }
    }

    public void UpdateScoreDisplay()
    {
        UpdateScoreCallback.Invoke(_scoreInfo.CurrentScore.ToString());
        UpdateNameCallback.Invoke(_scoreInfo.PlayerName);
    }

    public void ResetScore()
    {
        if (_scoreInfo != null)
        {
            _scoreInfo.ClearScore();
            UpdateScoreDisplay();
        }
    }

    public void UpdateName(string name)
    {
        if(_scoreInfo != null)
        {
            _scoreInfo.PlayerName = name;
            UpdateScoreDisplay();
        }
    }

    public void MakeLeaderBoardEntry()
    {
        LeaderBoard.MakeEntry(ScoreInfo.PlayerName, ScoreInfo.CurrentScore);
    }

    public void ClearLeaderBoard()
    {
        LeaderBoard.Clear();
    }
}