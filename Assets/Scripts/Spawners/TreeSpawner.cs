using System.Collections.Generic;
using StarAdventure.Interface;
using StarAdventure.Managers;
using UnityEngine;
using Random = UnityEngine.Random;
using Tree = StarAdventure.Obstacles.Tree;

namespace StarAdventure.Spawners
{
    public class TreeSpawner : MonoBehaviour, IGameEndListener
    {
        [SerializeField] private List<GameObject> treePrefabs = new List<GameObject>();
        [SerializeField] private float treeSpeed;
        [SerializeField] private float baseSpawnInterval;
        [SerializeField] private float spawnIntervalMin = 0.5f;
        [SerializeField] private float spawnIntervalScaleFactor = 0.001f;

        private float _currentSpawnInterval;
        private float _timeSinceLastSpawn;

        private bool _isGameEnded = false;
        void Start()
        {
            GameManager.Instance.RegisterListener(this);
            _currentSpawnInterval = baseSpawnInterval;
        }

        void Update()
        {
            ScaleTreeSpawnInterval();
        }

        private void ScaleTreeSpawnInterval()
        {
            float distanceTravelled = GameManager.Instance.DistanceTravelled;

            _currentSpawnInterval = Mathf.Max(spawnIntervalMin, baseSpawnInterval - (distanceTravelled * spawnIntervalScaleFactor));
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
            treeComponent.treeSpeed = treeSpeed;
        }

        public void OnGameEnd()
        {
            _isGameEnded = true;
        }
    }
}
