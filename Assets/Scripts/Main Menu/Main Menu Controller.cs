using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarAdventure.Main_Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject leaderboard;
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void OpenSetting()
        {

        }

        public void OpenLeaderboard()
        {
            leaderboard.SetActive(true);
        }
    }
}
