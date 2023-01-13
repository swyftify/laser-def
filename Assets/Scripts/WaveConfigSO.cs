using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField]
    List<GameObject> enemyPrefabs;

    [SerializeField]
    Transform pathPrefab;

    [SerializeField]
    float enemyMoveSpeed = 5f;

    [SerializeField]
    float timeBetweenEnemySpawns = 0.5f;

    [SerializeField]
    float spawnTimeVariance = 0f;

    [SerializeField]
    float minimumSpawnTime = 0.2f;

    public float GetEnemySpeed()
    {
        return enemyMoveSpeed;
    }

    public Transform GetStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWayPoints()
    {
        List<Transform> points = new List<Transform>();

        foreach (Transform point in pathPrefab)
        {
            points.Add(point);
        }

        return points;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyAtIndex(int index)
    {
        return enemyPrefabs[index];
    }

    public List<GameObject> GetEnemyList()
    {
        return enemyPrefabs;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - spawnTimeVariance,
            timeBetweenEnemySpawns + spawnTimeVariance
        );
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
