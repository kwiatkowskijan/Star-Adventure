using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour, IGameEndListener
{
    private BulletSpawner _bulletSpawner;

    public Bullet _bulletPrefab;
    [HideInInspector] public Vector3 _mousePosition;
    [SerializeField] private float _cooldown;
    private float _cooldownTime;
    [SerializeField] private AudioClip _shootingSFX;
    private AudioSource _audioSource;
    private bool _canShoot = true;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.RegisterListener(this);
    }

    void Update()
    {
        GetInput();
        GetCooldown();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && GetCooldown() >= _cooldown && _canShoot)
        {
            _cooldownTime = 0;
            AssignClip(_shootingSFX);
            PlayAudio();
            Shoot();
        }
    }

    private void Shoot()
    {
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

    public void OnGameEnd()
    {
        _canShoot = false;
    }
}
