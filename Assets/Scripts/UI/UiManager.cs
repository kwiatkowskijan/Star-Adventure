using StarAdventure.Interface;
using StarAdventure.Managers;
using TMPro;
using UnityEngine;

namespace StarAdventure.UI
{
    public class UiManager : MonoBehaviour, IGameEndListener
    {
        [SerializeField] private TMP_Text _points;
        [SerializeField] private TMP_Text _distanceTravelled;
        [SerializeField] private GameObject _endGameScreen;

        private void Start()
        {
            GameManager.Instance.RegisterListener(this);
        }

        private void Update()
        {
            UpdatePoints();
            UpdateDistanceTravlled();
        }

        private void UpdatePoints()
        {
            _points.SetText(GameManager.Instance.Points.ToString());
        }

        private void UpdateDistanceTravlled()
        {
            _distanceTravelled.SetText(GameManager.Instance.DistanceTravelled.ToString("0") + " m");
        }

        private void EndGameScreen()
        {
            _endGameScreen.SetActive(true);
        }

        public void OnGameEnd()
        {
            Invoke("EndGameScreen", 2f);
        }

    }
}
