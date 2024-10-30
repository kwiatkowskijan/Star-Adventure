using StarAdventure.Interface;
using StarAdventure.Managers;
using StarAdventure.Obstacles;
using UnityEngine;

namespace StarAdventure.Spawners
{
    public class CloudSpawner : Spawner<Cloud>, IGameEndListener
    {
        [SerializeField] private float cloudSpeed;

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.RegisterListener(this);
        }
        protected override void OnTakeFromPool(Cloud cloud)
        {
            base.OnTakeFromPool(cloud);
            MoveCloud(cloud);
        }

        private void MoveCloud(Cloud cloud)
        {
            var cloudScript = cloud.GetComponent<Cloud>();

            if (cloudScript != null)
            {
                cloudScript.Initialize(cloudSpeed);
            }
        }

        protected override void SetPool(Cloud cloud)
        {
            cloud.SetPool(pool);
        }

        public void OnGameEnd()
        {
            isGameEnded = true;
        }
    }
}