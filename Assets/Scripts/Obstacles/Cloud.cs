using System;
using StarAdventure.Interface;
using StarAdventure.Managers;
using UnityEngine;
using UnityEngine.Pool;

namespace StarAdventure.Obstacles
{
    public class Cloud : MonoBehaviour, IGameEndListener
    {
        [HideInInspector] public float cloudSpeed;
        private const int _damage = 1;
        private bool _isGameEnded = false;
        
        private ObjectPool<Cloud> cloudPool;
        public void SetPool(ObjectPool<Cloud> pool)
        {
            cloudPool = pool;
        }

        private void Start()
        {
            GameManager.Instance.RegisterListener(this);
        }
        
        void Update()
        {
            MoveCloud();
        }

        public void Initialize(float speed)
        {
            cloudSpeed = speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                GameManager.Instance.PlayerHealth -= _damage;
        }

        private void MoveCloud()
        {
            if(!_isGameEnded)
                transform.position += new Vector3(-cloudSpeed * Time.deltaTime, 0, 0);
        }

        private void StopCloud()
        {
            _isGameEnded = true;
        }
        private void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }

        public void OnGameEnd()
        {
            
        }
    }
}
