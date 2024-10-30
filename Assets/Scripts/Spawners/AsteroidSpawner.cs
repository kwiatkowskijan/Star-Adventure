using System.Collections.Generic;
using StarAdventure.Interface;
using StarAdventure.Managers;
using StarAdventure.Obstacles;
using UnityEngine;
using UnityEngine.Pool;

namespace StarAdventure.Spawners
{
    public class AsteroidSpawner : MonoBehaviour, IGameEndListener
    {
        private ObjectPool<Asteroid> _pool;

        [SerializeField] private List<Asteroid> asteroidPrefabs = new List<Asteroid>();
        [SerializeField] private GameObject player;
        [SerializeField] private float baseAsteroidSpeed;
        [SerializeField] private float baseSpawnInterval;
        [SerializeField] private float spawnIntervalMin;
        [SerializeField] private float speedScaleFactor = 0.01f;
        [SerializeField] private float spawnIntervalScaleFactor = 0.001f;
        [SerializeField] private int spawnMaxY;
        [SerializeField] private int spawnMinY;
        private bool _isGameEnded;

        private float _currentAsteroidSpeed;
        private float _currentSpawnInterval;
        private float _timeSinceLastSpawn;

        private void Start()
        {
            _currentAsteroidSpeed = baseAsteroidSpeed;
            _currentSpawnInterval = baseSpawnInterval;
            GameManager.Instance.RegisterListener(this);
            _pool = new ObjectPool<Asteroid>(CreateAsteroid, OnTakeAsteroidFromPool, OnReturnAsteroidToPool,
                OnDestroyAsteroid, true, 50, 100);
            _isGameEnded = false;
        }

        private void Update()
        {
            ScaleAsteroidInterval();
        }

        private Asteroid CreateAsteroid()
        {
            Asteroid asteroid = Instantiate(asteroidPrefabs[RandomAsteroidPrefab()], RandomSpawnPosition(),
                this.transform.rotation);
            ShootAsteroid(asteroid);
            asteroid.SetPool(_pool);

            return asteroid;
        }

        private void OnTakeAsteroidFromPool(Asteroid asteroid)
        {
            asteroid.transform.position = RandomSpawnPosition();
            asteroid.transform.rotation = Quaternion.identity;

            asteroid.gameObject.SetActive(true);
            asteroid.circleCollider.enabled = true;

            ShootAsteroid(asteroid);
        }

        private void OnReturnAsteroidToPool(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
        }

        private void OnDestroyAsteroid(Asteroid asteroid)
        {
            Destroy(asteroid);
        }

        private Vector2 RandomSpawnPosition()
        {
            Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(spawnMinY, spawnMaxY));
            return randomPosition;
        }

        private int RandomAsteroidPrefab()
        {
            return Random.Range(0, asteroidPrefabs.Count);
        }

        private void ScaleAsteroidInterval()
        {
            float distanceTravelled = GameManager.Instance.DistanceTravelled;
            _currentAsteroidSpeed = baseAsteroidSpeed + (distanceTravelled * speedScaleFactor);
            _currentSpawnInterval = Mathf.Max(spawnIntervalMin,
                baseSpawnInterval - (distanceTravelled * spawnIntervalScaleFactor));
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
            {
                _pool.Get();
                _timeSinceLastSpawn = 0f;
            }
        }

        private void ShootAsteroid(Asteroid asteroid)
        {
            var asteroidScript = asteroid.GetComponent<Asteroid>();

            if (asteroidScript != null)
            {
                asteroidScript.Initialize(player, _currentAsteroidSpeed);
            }
        }

        public void OnGameEnd()
        {
            _isGameEnded = true;
        }
    }
}