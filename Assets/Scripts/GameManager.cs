using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();

            return _instance;
        }
    }

    private int _points;
    public int Points
    {
        get { return _points; }

        set { _points = value; }
    }

    private float _distanceTravelled;
    public float DistanceTravelled
    {
        get { return _distanceTravelled; }

        set { _distanceTravelled = value; }
    }

    private GameManager()
    {
        _points = 0;
    }

}
