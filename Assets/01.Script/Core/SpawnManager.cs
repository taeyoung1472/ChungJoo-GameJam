using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleTon<SpawnManager>
{
    [SerializeField] private WavePerSpawn[] wavePerSpawnData;
    private int curWave;
    public void Start()
    {
        StartCoroutine(WaveSystem());
        StartCoroutine(Spawn());
    }

    IEnumerator WaveSystem()
    {
        while (curWave < wavePerSpawnData.Length)
        {
            yield return new WaitForSeconds(30);
            curWave++;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (curWave == wavePerSpawnData.Length)
            {
                GameManager.Instance.LoadEnding();
            }
            yield return new WaitForSeconds(wavePerSpawnData[curWave].spawnDelay);
            Enemy e = Instantiate(GenerateSpawnEnemy());
            e.transform.SetPositionAndRotation(Random.insideUnitCircle.normalized * 25, Quaternion.identity);
        }
    }

    Enemy GenerateSpawnEnemy()
    {
        Enemy e = null;
        int totalWeight = 0;
        int tempWeight = 0;
        int randIdx = 0;

        foreach (var w in wavePerSpawnData[curWave].spawnWights)
        {
            totalWeight += w.weight;
        }

        randIdx = Random.Range(1, totalWeight + 1);

        foreach (var w in wavePerSpawnData[curWave].spawnWights)
        {
            tempWeight += w.weight;
            if(randIdx <= tempWeight)
            {
                e = w.spawnEnemy;
                break;
            }
        }

        return e;
    }

    [System.Serializable]
    public class WavePerSpawn
    {
        public SpawnWight[] spawnWights;
        public float spawnDelay;
    }

    [System.Serializable]
    public class SpawnWight
    {
        public Enemy spawnEnemy;
        public int weight = 1;
    }
}