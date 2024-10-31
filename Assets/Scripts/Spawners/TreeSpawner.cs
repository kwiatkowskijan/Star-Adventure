using StarAdventure.Interface;
using StarAdventure.Managers;
using UnityEngine;
using Tree = StarAdventure.Obstacles.Tree;

namespace StarAdventure.Spawners
{
    public class TreeSpawner : Spawner<Tree>, IGameEndListener
    {
        [SerializeField] private float cloudSpeed;

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.RegisterListener(this);
        }
        protected override void OnTakeFromPool(Tree tree)
        {
            base.OnTakeFromPool(tree);
            Initialize(tree);
        }

        private void Initialize(Tree cloud)
        {
            var cloudScript = cloud.GetComponent<Tree>();

            if (cloudScript != null)
            {
                cloudScript.Initialize(cloudSpeed);
            }
        }

        protected override void SetPool(Tree tree)
        {
            tree.SetPool(pool);
        }

        public void OnGameEnd()
        {
            isGameEnded = true;
        }
    }
}
