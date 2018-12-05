using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
public class ScoreManegerSaveLoadComponent : BaseSaveLoadComponent
{
    [SerializeField] private ScoreManager scoreManager = null;

    public override void Load(object @object)
    {
        if(@object is ScoreInfo)
        {
            ScoreInfo scoreInfo = @object as ScoreInfo;
            scoreManager.ScoreInfo.PlayerName = scoreInfo.PlayerName;
            scoreManager.ScoreInfo.CurrentScore = scoreInfo.CurrentScore;
            scoreManager.UpdateScoreDisplay();
        }
    }

    public override object Save()
    {
        return scoreManager.ScoreInfo;
    }

    private void Reset()
    {
        scoreManager = GetComponent<ScoreManager>();
    }
}
