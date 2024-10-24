using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace StarAdventure.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private float _distance;
        [SerializeField] private TMP_InputField name;

        public UnityEvent<string, int> submitScoreEvent;

        public void SubmitScore()
        {
            _distance = GameManager.Instance.DistanceTravelled;

            Debug.Log(name + ": " + _distance);

            submitScoreEvent.Invoke(name.text, (int)_distance);
        }
    }
}
