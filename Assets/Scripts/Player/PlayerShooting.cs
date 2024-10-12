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

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
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
}
