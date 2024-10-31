using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using StarAdventure.Managers;

namespace StarAdventure.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected ObjectPool<T> pool;
        protected bool isGameEnded;
        
        [SerializeField] protected List<T> spawnedObjects = new List<T>();
        [SerializeField] protected float baseObjectSpeed;
        [SerializeField] protected float baseSpawnInterval;
        [SerializeField] protected float minSpawnInterval;
        [SerializeField] protected float speedScaleFactor;
        [SerializeField] protected float spawnIntervalFactor;
        [SerializeField] protected float maxSpawnY;
        [SerializeField] protected float minSpawnY;
        
        protected float currentObjectSpeed;
        private float currentSpawnInterval;
        private float timeSinceLastSpawn;

        protected virtual void Start()
        {
            currentObjectSpeed = baseObjectSpeed;
            currentSpawnInterval = baseSpawnInterval;
            pool = new ObjectPool<T>(CreateObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, true, 10, 20);
        }

        protected virtual void Update()
        {
            ScaleSpawnInterval();
        }

        protected virtual T CreateObject()
        {
            T obj = Instantiate(spawnedObjects[RandomObject()], RandomSpawnPosition(), this.transform.rotation);
            SetPool(obj);
            return obj;    
        }

        protected virtual void OnTakeFromPool(T obj)
        {
            obj.transform.position = RandomSpawnPosition();
            obj.transform.rotation = Quaternion.identity;
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyObject(T obj)
        {
            Destroy(obj);
        }

        private int RandomObject()
        {
            return Random.Range(0, spawnedObjects.Count);
        }

        private Vector2 RandomSpawnPosition()
        {
            Vector2 randomPosition = new Vector2(this.transform.position.x, Random.Range(minSpawnY, maxSpawnY));
            return randomPosition;
        }

        protected virtual void ScaleSpawnInterval()
        {
            float distanceTravelled = GameManager.Instance.DistanceTravelled;
            
            currentObjectSpeed = baseObjectSpeed + (distanceTravelled * speedScaleFactor);
            currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - (distanceTravelled * spawnIntervalFactor));
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= currentSpawnInterval && !isGameEnded)
            {
                pool.Get();
                timeSinceLastSpawn = 0;
            }
        }
        
        protected abstract void SetPool(T objectToPool);
    }
}
