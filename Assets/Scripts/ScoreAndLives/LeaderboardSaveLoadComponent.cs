using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
public class LeaderboardSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] private ScoreManager scoreManager = null;

    public override void Load(object @object)
    {
        if (@object is LeaderBoard)
        {
            LeaderBoard leaderBoard = @object as LeaderBoard;
            scoreManager.LeaderBoard.Entries = leaderBoard.Entries;
            scoreManager.UpdateScoreDisplay();
        }
    }

    public override object Save()
    {
        return scoreManager.LeaderBoard;
    }

    private void Reset()
    {
        scoreManager = GetComponent<ScoreManager>();
    }
}
