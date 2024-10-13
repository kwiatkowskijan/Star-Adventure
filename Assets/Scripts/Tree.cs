using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] float _treeSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        MoveTree();
    }

    private void MoveTree()
    {
        transform.position += new Vector3(-_treeSpeed * Time.deltaTime, 0, 0);
    }
}
