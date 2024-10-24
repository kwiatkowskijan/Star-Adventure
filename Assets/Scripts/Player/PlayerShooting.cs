using StarAdventure.Interface;
using UnityEngine;
using StarAdventure.Managers;

namespace StarAdventure.Player
{
    public class PlayerShooting : MonoBehaviour, IGameEndListener
    {
        private BulletSpawner _bulletSpawner;

        public Bullet bulletPrefab;
        [HideInInspector] public Vector3 mousePosition;
        [SerializeField] private float cooldown;
        private float _cooldownTime;
        [SerializeField] private AudioClip shootingSfx;
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
            if (Input.GetMouseButtonDown(0) && GetCooldown() >= cooldown && _canShoot)
            {
                _cooldownTime = 0;
                AssignClip(shootingSfx);
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
}
