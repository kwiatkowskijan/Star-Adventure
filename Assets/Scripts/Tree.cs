using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [HideInInspector] public float treeSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        MoveTree();
    }

    private void MoveTree()
    {
        transform.position += new Vector3(-treeSpeed * Time.deltaTime, 0, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
