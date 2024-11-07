using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

namespace StarAdventure.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> distances;

        private string _leaderboardPublicKey = "8a08b07858bf8aab8497541a32bac1d9dddd7236afc0de9b402dcd422dbbd874";

        private void Start()
        {
            GetLeaderboard();
        }

        private void GetLeaderboard()
        {
            LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, ((msg) =>
            {
                int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;

                for (int i = 0; i < loopLength; ++i)
                {
                    names[i].text = msg[i].Username;
                    distances[i].text = msg[i].Score.ToString() + " m";
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
}
