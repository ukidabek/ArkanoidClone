using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text _name = null;
    [SerializeField] private Text _score = null;

    public void SetDisplay(LeaderBoard.Entry entry)
    {
        _name.text = entry.Name;
        _score.text = entry.Score.ToString();
    }
}
