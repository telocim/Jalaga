using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves()
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                //spawn each enemy inside parent EnemySpawner object
                Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.identity, transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
