using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour, IGameEndListener
{
    [SerializeField] private float _speed;
    private Vector2 _startPosition;
    private float _repeatWidth;

    private void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<BoxCollider2D>().size.x * .5f;
        GameManager.Instance.RegisterListener(this);
    }
    private void Update()
    {
        MoveBgLeft(_speed);
        RepeatBg();
        CountDistance();
    }

    private void MoveBgLeft(float speed)
    {
        this.transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    private void RepeatBg()
    {
        if(this.transform.position.x < _startPosition.x - _repeatWidth)
        {
            transform.position = _startPosition;
        }
    }

    private void CountDistance()
    {
        GameManager.Instance.DistanceTravelled += _speed * Time.deltaTime;
    }

    private void StopBg()
    {
        _speed = 0;
    }

    public void OnGameEnd()
    {
        StopBg();
    }
}
