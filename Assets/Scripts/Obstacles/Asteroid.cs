using StarAdventure.Managers;
using StarAdventure.Player;
using UnityEngine;
using UnityEngine.Pool;

namespace StarAdventure.Obstacles
{
    public class Asteroid : MonoBehaviour
    {
        private float _asteroidSpeed;
        private GameObject _player;
        private Vector2 _lastPlayerPosition;
        private int _health;
        private Animator _animator;
        private int _point;
        private bool _dead;
        private Vector2 _direction;
        private int _damage;
        private ObjectPool<Asteroid> _pool;
        public CircleCollider2D circleCollider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            GetLastPlayerPosition();
            _health = 1;
            _point = 1;
            _dead = false;
            _damage = 1;
        }

        private void OnEnable()
        {
            _dead = false;
        }

        private void Update()
        {
            ShootAsteroid();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                GetDamage(bullet.damage);
            }

            if (collision.gameObject.CompareTag("Player"))
                GameManager.Instance.PlayerHealth -= _damage;
        }

        public void SetPool(ObjectPool<Asteroid> pool)
        {
            _pool = pool;
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

            if (_direction == Vector2.zero)
            {
                _direction = (_lastPlayerPosition - (Vector2)transform.position).normalized;
            }

            transform.position += (Vector3)(_direction * step);
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
                GameManager.Instance.Points += _point;

            this.circleCollider.enabled = false;
            _dead = true;
            _animator.SetTrigger("Death");
            Invoke(nameof(Destroy), 0.7f);
        }

        private void Destroy()
        {
            _pool.Release(this);
        }
    }
}
