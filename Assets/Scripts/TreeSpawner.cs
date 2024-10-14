using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour, IGameEndListener
{
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private float _treeSpeed;

    void Start()
    {
        SpawnTree();
    }

    void Update()
    {
        
    }

    private void SpawnTree()
    {
        var tree = Instantiate( _treePrefab, this.transform.position, Quaternion.identity);
        Tree treeComponent = tree.GetComponent<Tree>();
        treeComponent.treeSpeed = _treeSpeed;
    }

    public void OnGameEnd()
    {

    }
}
