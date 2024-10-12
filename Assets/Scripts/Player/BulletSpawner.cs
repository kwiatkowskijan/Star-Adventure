using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet> pool;
    private PlayerShooting _playerShooting;

    private void Awake()
    {
        _playerShooting = GetComponent<PlayerShooting>();
    }

    private void Start()
    {
        pool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 50, 100);
    }

    private void Update()
    {
        
    }

    private Bullet CreateBullet()
    {
        //spawn new instance of the bullet
        Bullet bullet = Instantiate(_playerShooting._bulletPrefab, this.transform.position + new Vector3(.5f, 0, 0), Quaternion.identity);

        //asign the bullet's pool
        bullet.SetPool(pool);

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        //set transform and rotation
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = Quaternion.identity;

        //activate
        bullet.gameObject.SetActive(true);

        // Get the mouse position and set direction for the bullet
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Set the bullet's direction based on the mouse position
        bullet.SetDestination(mousePosition);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        //deactivate
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet);
    }
}
