using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IGameEndListener
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private float _verticalSpeed;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _deathClip;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
        GameManager.Instance.RegisterListener(this);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _verticalSpeed = Input.GetAxis("Vertical") * _speed;
        _rb.velocity = new Vector2(0, _verticalSpeed);

        if(_rb.velocity.magnitude > 0 || _rb.velocity.magnitude < 0 )
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    private void DeathAnimation()
    {
        _animator.SetTrigger("Death");
    }

    private void AssignClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }

    private void PlayAudio()
    {
        _audioSource.Play();
    }

    public void OnGameEnd()
    {
        AssignClip(_deathClip);
        PlayAudio();
        DeathAnimation();
    }
}
