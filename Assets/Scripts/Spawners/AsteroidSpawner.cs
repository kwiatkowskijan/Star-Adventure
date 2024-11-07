using StarAdventure.Interface;
using StarAdventure.Managers;
using StarAdventure.Obstacles;
using UnityEngine;

namespace StarAdventure.Spawners
{
    public class AsteroidSpawner : Spawner<Asteroid>, IGameEndListener
    {
        [SerializeField] private Transform player;
        private Vector2 _lastPlayerPosition;

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.RegisterListener(this);
        }
        
        protected override void OnTakeFromPool(Asteroid asteroid)
        {
            Initialize(asteroid);
            base.OnTakeFromPool(asteroid);
        }

        private void Initialize(Asteroid asteroid)
        {
            var asteroidScript = asteroid.GetComponent<Asteroid>();

            if (asteroidScript != null)
            {
                asteroidScript.Initialize(currentObjectSpeed, GetLastPlayerPosition());
            }
        }
        
        private Vector3 GetLastPlayerPosition()
        {
            _lastPlayerPosition = player.position;
            Debug.Log(_lastPlayerPosition);
            return _lastPlayerPosition;
        }

        protected override void SetPool(Asteroid asteroid)
        {
            asteroid.SetPool(pool);
        }

        public void OnGameEnd()
        {
            isGameEnded = true;
        }
    }
}