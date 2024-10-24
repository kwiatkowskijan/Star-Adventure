using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour, IGameEndListener
{
    [SerializeField] private List<GameObject> treePrefabs = new List<GameObject>();
    [SerializeField] private float _treeSpeed;

    [SerializeField] private float _baseSpawnInterval;
    [SerializeField] private float _spawnIntervalMin = 0.5f;
    [SerializeField] private float _spawnIntervalScaleFactor = 0.001f;

    private float _currentSpawnInterval;
    private float _timeSinceLastSpawn;

    private bool _isGameEnded = false;
    void Start()
    {
        GameManager.Instance.RegisterListener(this);
        _currentSpawnInterval = _baseSpawnInterval;
    }

    void Update()
    {
        ScaleTreeSpawnInterval();
    }

    private void ScaleTreeSpawnInterval()
    {
        float distanceTravelled = GameManager.Instance.DistanceTravelled;

        _currentSpawnInterval = Mathf.Max(_spawnIntervalMin, _baseSpawnInterval - (distanceTravelled * _spawnIntervalScaleFactor));
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _currentSpawnInterval && !_isGameEnded)
        {
            SpawnTree();
            _timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnTree()
    {
        var i = Random.Range(0, treePrefabs.Count);
        var tree = Instantiate(treePrefabs[i], this.transform.position, Quaternion.identity);
        Tree treeComponent = tree.GetComponent<Tree>();
        treeComponent.treeSpeed = _treeSpeed;
    }

    public void OnGameEnd()
    {
        _isGameEnded = true;
    }
}
