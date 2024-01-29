using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WavesConfig> wavesConfigs;
    [SerializeField] float timeBetweenEnemyWaves = 0f;
    [SerializeField] bool isLooping;
    WavesConfig currentWave;
    void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }
    public WavesConfig GetCurrentWave()
    {
        return currentWave;
    } 
    IEnumerator SpawnEnemiesWaves()
    {
        do
        {
            foreach(WavesConfig wave in wavesConfigs)
            {
                currentWave = wave;
                for(int i = 0 ; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWayPoint().position, 
                    Quaternion.Euler(0,0,180), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenEnemyWaves);
            }
        }while(isLooping);
    }

}
