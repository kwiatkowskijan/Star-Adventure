using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IGameEndListener
{
    [SerializeField] private List<GameObject> _asteroidPrefabs = new List<GameObject>();
    [SerializeField] private float _baseAsteroidSpeed;
    [SerializeField] private GameObject _player;
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
    }

    private void Update()
    {
        ScaleAsteroidInterval();
    }

    private void SpawnAsteroid()
    {
        int randomIndex = Random.Range(0, _asteroidPrefabs.Count);
        Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(_spawnMinY, _spawnMaxY));
        var asteroid = Instantiate(_asteroidPrefabs[randomIndex], randomPosition, this.transform.rotation);
        ShootAsteroid(asteroid);
    }

    private void ShootAsteroid(GameObject asteroid)
    {
        var asteroidScript = asteroid.GetComponent<Asteroid>();

        if (asteroidScript != null)
        {
            asteroidScript.Initialize(_player, _currentAsteroidSpeed);
        }
    }

    private void ScaleAsteroidInterval()
    {
        float distanceTravelled = GameManager.Instance.DistanceTravelled;
        _currentAsteroidSpeed = _baseAsteroidSpeed + (distanceTravelled * _speedScaleFactor);
        _currentSpawnInterval = Mathf.Max(_spawnIntervalMin, _baseSpawnInterval - (distanceTravelled * _spawnIntervalScaleFactor));
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
        {
            SpawnAsteroid();
            _timeSinceLastSpawn = 0f; 
        }

    }

    public void OnGameEnd()
    {
        _isGameEnded = true;
    }
}
