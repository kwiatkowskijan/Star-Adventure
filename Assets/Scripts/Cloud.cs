using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Cloud : MonoBehaviour
{
    private ObjectPool<Cloud> cloudPool;
    public void SetPool(ObjectPool<Cloud> pool)
    {
        cloudPool = pool;
    }
    
}
