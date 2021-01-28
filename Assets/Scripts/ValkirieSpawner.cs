using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class ValkirieSpawner : SingletonClass<ValkirieSpawner>, IPooledObject

{
    [SerializeField] private Tag valkirieTag;
    [SerializeField] private Transform spawnPoint;
    private ObjectPooler _objectPooler;
    public override void Awake()
    {
        _objectPooler = ObjectPooler.Instance;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectPooler.Instance.SpawnFromPool(valkirieTag, spawnPoint.position, Quaternion.identity);
    }
    
    public void OnObjectSpawn()
    {
        
    }
    
}
