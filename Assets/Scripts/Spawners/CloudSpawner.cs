using System.Collections.Generic;
using StarAdventure.Interface;
using StarAdventure.Managers;
using StarAdventure.Obstacles;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace StarAdventure.Spawners
{
    public class CloudSpawner : MonoBehaviour, IGameEndListener
    {
        private ObjectPool<Cloud> _pool;
        [SerializeField] private List<Cloud> cloudPrefabs = new List<Cloud>();
        [SerializeField] private float cloudSpeed;
        [SerializeField] private float baseSpawnInterval;
        [SerializeField] private float spawnIntervalMin = 0.5f;
        [SerializeField] private float spawnIntervalScaleFactor = 0.001f;
        [SerializeField] private int spawnMaxY;
        [SerializeField] private int spawnMinY;

        private float _currentSpawnInterval;
        private float _timeSinceLastSpawn;

        private bool _isGameEnded;

        private void Start()
        {
            GameManager.Instance.RegisterListener(this);
            _currentSpawnInterval = baseSpawnInterval;
            _pool = new ObjectPool<Cloud>(CreateCloud, OnTakeCloudFormPool, OnReturnAsteroidToPool, OnDestroyAsteroid,
                true, 10, 20);
        }

        private void Update()
        {
            ScaleCloudSpawnInterval();
        }

        private Cloud CreateCloud()
        {
            int i = Random.Range(0, cloudPrefabs.Count);
            Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(spawnMinY, spawnMaxY));
            Cloud cloud = Instantiate(cloudPrefabs[i], randomPosition, this.transform.rotation);

            cloud.SetPool(_pool);

            return cloud;
        }

        private void OnTakeCloudFormPool(Cloud cloud)
        {
            cloud.transform.position = this.transform.position;
            cloud.transform.rotation = Quaternion.identity;
            cloud.gameObject.SetActive(true);
        }

        private void OnReturnAsteroidToPool(Cloud cloud)
        {
            cloud.gameObject.SetActive(false);
        }

        private void OnDestroyAsteroid(Cloud cloud)
        {
            Destroy(cloud);
        }

        private void ScaleCloudSpawnInterval()
        {
            float distanceTravelled = GameManager.Instance.DistanceTravelled;

            _currentSpawnInterval = Mathf.Max(spawnIntervalMin,
                baseSpawnInterval - (distanceTravelled * spawnIntervalScaleFactor));
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
            {
                _timeSinceLastSpawn = 0f;
            }
        }

        public void OnGameEnd()
        {
            _isGameEnded = true;
        }
    }
}