using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _points;
    [SerializeField] private TMP_Text _distanceTravelled;

    private void Update()
    {
        UpdatePoints();
        UpdateDistanceTravlled();
    }

    void UpdatePoints()
    {
        _points.SetText(GameManager.Instance.Points.ToString());
    }

    void UpdateDistanceTravlled()
    {
        _distanceTravelled.SetText(GameManager.Instance.DistanceTravelled.ToString("0") + " m");
    }
}
