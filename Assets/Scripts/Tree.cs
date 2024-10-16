using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IGameEndListener
{
    [HideInInspector] public float treeSpeed;
    private int _damage = 1;
    private bool _isGameEnded = false;
    void Start()
    {
        GameManager.Instance.RegisterListener(this);
    }

    void Update()
    {
        MoveTree();
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
        Destroy(this.gameObject);
    }

    public void OnGameEnd()
    {
        StopTree();
    }
}
