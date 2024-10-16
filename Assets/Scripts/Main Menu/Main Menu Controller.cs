using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboard;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    {

    }

    public void OpenLeaderboard()
    {
        _leaderboard.SetActive(true);
    }
}
