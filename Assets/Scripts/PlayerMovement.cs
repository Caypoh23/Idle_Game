using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPooledObject
{
    [SerializeField] private Rigidbody2D valkirieRigidbody2D;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Tag valkirieTag;
    private float movement = 5;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        valkirieRigidbody2D.velocity = new Vector2( movement * speed, valkirieRigidbody2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectPooler.Instance.SpawnFromPool(valkirieTag, SpawnPoint.position, Quaternion.identity);
    }

    public void OnObjectSpawn()
    {
        Debug.Log("Spawned");
    }
}
