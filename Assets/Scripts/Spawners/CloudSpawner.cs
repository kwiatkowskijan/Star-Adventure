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
            Cloud cloud = Instantiate(cloudPrefabs[RandomCloudPrefab()], RandomSpawnPosition(), this.transform.rotation);

            cloud.SetPool(_pool);

            return cloud;
        }

        private void OnTakeCloudFormPool(Cloud cloud)
        {
            cloud.transform.position = RandomSpawnPosition();
            cloud.transform.rotation = Quaternion.identity;
            cloud.gameObject.SetActive(true);
            
            MoveCloud(cloud);
        }

        private void OnReturnAsteroidToPool(Cloud cloud)
        {
            cloud.gameObject.SetActive(false);
        }

        private void OnDestroyAsteroid(Cloud cloud)
        {
            Destroy(cloud);
        }
        
        private Vector2 RandomSpawnPosition()
        {
            Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(spawnMinY, spawnMaxY));
            return randomPosition;
        }

        private int RandomCloudPrefab()
        {
            return Random.Range(0, cloudPrefabs.Count);
        }

        private void ScaleCloudSpawnInterval()
        {
            float distanceTravelled = GameManager.Instance.DistanceTravelled;

            _currentSpawnInterval = Mathf.Max(spawnIntervalMin,
                baseSpawnInterval - (distanceTravelled * spawnIntervalScaleFactor));
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
            {
                _pool.Get();
                _timeSinceLastSpawn = 0f;
            }
        }

        private void MoveCloud(Cloud cloud)
        {
            var cloudScript = cloud.GetComponent<Cloud>();

            if (cloudScript != null)
            {
                cloudScript.Initialize(cloudSpeed);
            }
        }

        public void OnGameEnd()
        {
            _isGameEnded = true;
        }
    }
}