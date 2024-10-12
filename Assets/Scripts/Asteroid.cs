using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _asteroidSpeed;
    private GameObject _player;
    private Vector2 _lastPlayerPosition;
    private int _health = 1;
    private Animator _animator;
    private int point = 1;
    private bool _dead = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GetLastPlayerPosition();
    }

    private void Update()
    {
        ShootAsteroid();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("trafiono");
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            GetDamage(bullet.damage);
        }
    }

    public void Initialize(GameObject player, float speed)
    {
        _player = player;
        _asteroidSpeed = speed;
    }


    private Vector2 GetLastPlayerPosition()
    {
        _lastPlayerPosition = _player.transform.position;
        return _lastPlayerPosition;
    }

    private void ShootAsteroid()
    {
        var step = _asteroidSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _lastPlayerPosition, step);
    }

    public void GetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        if(!_dead)
            GameManager.Instance.Points += point;
        
        _dead = true;
        _animator.SetTrigger("Death");
        Invoke("Destroy", 0.7f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
