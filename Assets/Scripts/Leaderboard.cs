using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _distances;

    private string _leaderboardPublicKey = "8a08b07858bf8aab8497541a32bac1d9dddd7236afc0de9b402dcd422dbbd874";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, ((msg) =>
        {
            int loopLength = (msg.Length < _names.Count) ? msg.Length : _names.Count;

            for (int i = 0; i < loopLength; ++i)
            {
                _names[i].text = msg[i].Username;
                _distances[i].text = msg[i].Score.ToString() + " m";
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
