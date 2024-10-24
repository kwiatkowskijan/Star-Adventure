using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour, IGameEndListener
{
    public ObjectPool<Cloud> pool;
    [SerializeField] private List<Cloud> cloudPrefabs = new List<Cloud>();
    [SerializeField] private float _cloudSpeed;
    [SerializeField] private float _baseSpawnInterval;
    [SerializeField] private float _spawnIntervalMin = 0.5f;
    [SerializeField] private float _spawnIntervalScaleFactor = 0.001f;
    [SerializeField] private int _spawnMaxY;
    [SerializeField] private int _spawnMinY;
    
    private float _currentSpawnInterval;
    private float _timeSinceLastSpawn;
    
    private bool _isGameEnded = false;

    private void Start()
    {
        GameManager.Instance.RegisterListener(this);
        _currentSpawnInterval = _baseSpawnInterval;
        pool = new ObjectPool<Cloud>(CreateCloud, OnTakeCloudFormPool, OnReturnAsteroidToPool, OnDestroyAsteroid, true,
            10, 20);
    }

    private void Update()
    {
        ScaleCloudSpawnInterval();
    }

    private Cloud CreateCloud()
    {
        int i = Random.Range(0, cloudPrefabs.Count);
        Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(_spawnMinY, _spawnMaxY));
        Cloud cloud = Instantiate(cloudPrefabs[i], randomPosition, this.transform.rotation);

        cloud.SetPool(pool);

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

        _currentSpawnInterval = Mathf.Max(_spawnIntervalMin, _baseSpawnInterval - (distanceTravelled * _spawnIntervalScaleFactor));
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
