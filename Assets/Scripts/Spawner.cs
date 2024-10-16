using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour, IGameEndListener
{
    public ObjectPool<Asteroid> pool;

    [SerializeField] private List<Asteroid> _asteroidPrefabs = new List<Asteroid>();
    [SerializeField] private GameObject _player;
    [SerializeField] private float _baseAsteroidSpeed;
    [SerializeField] private float _baseSpawnInterval;
    [SerializeField] private float _spawnIntervalMin = 0.5f;
    [SerializeField] private float _speedScaleFactor = 0.01f;
    [SerializeField] private float _spawnIntervalScaleFactor = 0.001f;
    [SerializeField] private int _spawnMaxY;
    [SerializeField] private int _spawnMinY;
    private bool _isGameEnded = false;

    private float _currentAsteroidSpeed;
    private float _currentSpawnInterval;
    private float _timeSinceLastSpawn;

    private void Start()
    {
        _currentAsteroidSpeed = _baseAsteroidSpeed;
        _currentSpawnInterval = _baseSpawnInterval;
        GameManager.Instance.RegisterListener(this);
        pool = new ObjectPool<Asteroid>(CreateAsteroid, OnTakeAsteroidFromPool, OnReturnAsteroidToPool, OnDestroyAsteroid, true, 50, 100);
    }

    private void Update()
    {
        ScaleAsteroidInterval();
    }

    private Asteroid CreateAsteroid()
    {
        int randomIndex = Random.Range(0, _asteroidPrefabs.Count);
        Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(_spawnMinY, _spawnMaxY));
        Asteroid asteroid = Instantiate(_asteroidPrefabs[randomIndex], randomPosition, this.transform.rotation);

        asteroid.SetPool(pool);

        return asteroid;
    }

    private void OnTakeAsteroidFromPool(Asteroid asteroid)
    {
        asteroid.transform.position = this.transform.position;
        asteroid.transform.rotation = Quaternion.identity;

        asteroid.gameObject.SetActive(true);
        asteroid._circleCollider.enabled = true;

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

    private void ScaleAsteroidInterval()
    {
        float distanceTravelled = GameManager.Instance.DistanceTravelled;
        _currentAsteroidSpeed = _baseAsteroidSpeed + (distanceTravelled * _speedScaleFactor);
        _currentSpawnInterval = Mathf.Max(_spawnIntervalMin, _baseSpawnInterval - (distanceTravelled * _spawnIntervalScaleFactor));
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
        {
            pool.Get();
            _timeSinceLastSpawn = 0f;
        }
    }

    private void ShootAsteroid(Asteroid asteroid)
    {
        var asteroidScript = asteroid.GetComponent<Asteroid>();

        if (asteroidScript != null)
        {
            asteroidScript.Initialize(_player, _currentAsteroidSpeed);
        }
    }

    public void OnGameEnd()
    {
        _isGameEnded = true;
    }
}
