using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Transform nextLevelEntryPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = nextLevelEntryPoint.position;
    }
}
