using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _asteroidPrefabs = new List<GameObject>();
    [SerializeField] private float _baseAsteroidSpeed;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _baseSpawnInterval;
    [SerializeField] private float _spawnIntervalMin = 0.5f; // Minimalny interwa³ spawnu
    [SerializeField] private float _speedScaleFactor = 0.01f; // Jak szybko roœnie prêdkoœæ w zale¿noœci od dystansu
    [SerializeField] private float _spawnIntervalScaleFactor = 0.001f; // Jak szybko zmniejsza siê interwa³ w zale¿noœci od dystansu
    [SerializeField] private int _spawnMaxY;
    [SerializeField] private int _spawnMinY;

    private float _currentAsteroidSpeed;
    private float _currentSpawnInterval;
    private float _timeSinceLastSpawn;

    private void Start()
    {
        _currentAsteroidSpeed = _baseAsteroidSpeed;
        _currentSpawnInterval = _baseSpawnInterval;
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

        // Sprawdzenie, czy min¹³ czas na spawnowanie nowej asteroidy
        if (_timeSinceLastSpawn >= _currentSpawnInterval)
        {
            SpawnAsteroid();
            _timeSinceLastSpawn = 0f; // Zresetuj czas od ostatniego spawnu
        }

    }
}
