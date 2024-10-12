using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public int damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _bulletSpeed;  // Prêdkoœæ pocisku
    private Rigidbody2D _rb;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateBulletAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _pool.Release(this);
        }
    }

    public void SetDestination(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        _pool = pool;   
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pool.Release(this);
    }

    
}
