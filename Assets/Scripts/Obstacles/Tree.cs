using StarAdventure.Interface;
using UnityEngine;
using StarAdventure.Managers;
using UnityEngine.Pool;

namespace StarAdventure.Obstacles
{
    public class Tree : MonoBehaviour, IGameEndListener
    {
        [HideInInspector] public float treeSpeed;
        private const int _damage = 1;
        private bool _isGameEnded = false;
        private ObjectPool<Tree> _objectPool;
        void Start()
        {
            GameManager.Instance.RegisterListener(this);
        }

        void Update()
        {
            MoveTree();
        }

        public void SetPool(ObjectPool<Tree> objectPool)
        {
            _objectPool = objectPool;
        }
        
        public void Initialize(float speed)
        {
            treeSpeed = speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                GameManager.Instance.PlayerHealth -= _damage;
        }

        private void MoveTree()
        {
            if(!_isGameEnded)
                transform.position += new Vector3(-treeSpeed * Time.deltaTime, 0, 0);
        }

        private void StopTree()
        {
            _isGameEnded = true;
        }
        private void OnBecameInvisible()
        {
            _objectPool.Release(this);
        }

        public void OnGameEnd()
        {
            StopTree();
        }
    }
}
