using UnityEngine;
using UnityEngine.Pool;

namespace StarAdventure.Obstacles
{
    public class Cloud : MonoBehaviour
    {
        private ObjectPool<Cloud> cloudPool;
        public void SetPool(ObjectPool<Cloud> pool)
        {
            cloudPool = pool;
        }
    
    }
}
