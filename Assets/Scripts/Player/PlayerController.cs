using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private float _verticalSpeed;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
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
}
