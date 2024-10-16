using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private float _distance;
    [SerializeField] private TMP_InputField _name;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        _distance = GameManager.Instance.DistanceTravelled;

        Debug.Log(_name + ": " + _distance);

        submitScoreEvent.Invoke(_name.text, (int)_distance);
    }
}
