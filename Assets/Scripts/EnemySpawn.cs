using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    List<WaveConfigSO> waveConfigs;

    [SerializeField]
    float timeBetweenWaves = 3f;
    WaveConfigSO currentWave;

    [SerializeField]
    bool isLooping = true;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        do
        {
            foreach (var wave in waveConfigs)
            {
                currentWave = wave;
                StartCoroutine(SpawnEnemyWithDelay());
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        } while (isLooping);
    }

    IEnumerator SpawnEnemyWithDelay()
    {
        foreach (var enemy in currentWave.GetEnemyList())
        {
            Instantiate(
                enemy,
                currentWave.GetStartingWayPoint().position,
                Quaternion.identity,
                transform
            );
            yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
