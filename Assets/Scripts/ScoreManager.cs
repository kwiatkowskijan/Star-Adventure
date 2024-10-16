using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private float _score;
    [SerializeField] private TMP_InputField _name;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        _score = GameManager.Instance.DistanceTravelled;
        submitScoreEvent.Invoke(_name.text, (int)_score);
    }
}
