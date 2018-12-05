using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private Transform scoreDisplayParent;
    [SerializeField] private LeaderBoardScoreDisplay _leaderBoardScoreDisplayPrefab = null;
    private List<LeaderBoardScoreDisplay> leaderBoardScoreDisplays = new List<LeaderBoardScoreDisplay>();

    private void OnEnable()
    {
        if(ScoreManager.Instance != null)
        {
            foreach (var item in ScoreManager.Instance.LeaderBoard.Entries)
                GetFreeDisplay().SetDisplay(item);
        }
    }

    private void OnDisable()
    {
        foreach (var item in leaderBoardScoreDisplays)
        {
            item.gameObject.SetActive(false);
        }
    }

    private LeaderBoardScoreDisplay GetFreeDisplay()
    {
        LeaderBoardScoreDisplay display = null;
        foreach (var item in leaderBoardScoreDisplays)
        {
            if (!item.gameObject.activeSelf)
            {
                display = item;
                break;
            }
        }

        if(display == null)
        {
            display = Instantiate(_leaderBoardScoreDisplayPrefab);
            display.transform.SetParent(scoreDisplayParent);
            leaderBoardScoreDisplays.Add(display);
        }
        display.gameObject.SetActive(true);
        return display;
    }
}
