using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject _prefab;

    public Transform spawnPosition;
    public Transform spawnPosition2;
    public Transform spawnPosition3;
    public Transform spawnPosition4;

    private Transform currentSpawnPos;

    public float spawnInterval = 5f;
    private float _currentTime = 0f;

    void Update()
    {
        // Random Spawn locations

        int randomPos = Random.Range(1, 5);
        switch(randomPos)
        {
            case 1: currentSpawnPos = spawnPosition;
                break;
            case 2:
                currentSpawnPos = spawnPosition2;
                break;
            case 3:
                currentSpawnPos = spawnPosition3;
                break;
            case 4:
                currentSpawnPos = spawnPosition4;
                break;
        }


        _currentTime += Time.deltaTime;

        if(_currentTime > spawnInterval)
        {
            _currentTime = 0f;

            Instantiate(
                original: _prefab,
                position: currentSpawnPos.position,
                rotation: Quaternion.identity
                );
        }
    }
}
