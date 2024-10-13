using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private BulletSpawner _bulletSpawner;

    public Bullet _bulletPrefab;
    [HideInInspector] public Vector3 _mousePosition;
    [SerializeField] private float _cooldown;
    private float _cooldownTime;
    [SerializeField] private AudioClip _shootingSFX;
    private AudioSource _audioSource;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        GetInput();
        GetCooldown();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && GetCooldown() >= _cooldown)
        {
            _cooldownTime = 0;
            AssignClip(_shootingSFX);
            PlayAudio();
            Shoot();
        }
    }

    private void Shoot()
    {
        //spawn bullet
        _bulletSpawner.pool.Get();
    }

    private float GetCooldown()
    {
        _cooldownTime += Time.deltaTime;
        return _cooldownTime;
    }

    private void AssignClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }

    private void PlayAudio()
    {
        _audioSource.Play();
    }
}
