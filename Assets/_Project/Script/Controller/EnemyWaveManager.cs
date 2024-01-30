using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[System.Serializable]
public class TroopInfo
{
    public GameObject troopPrefab;
    public int troopCount;
}

[System.Serializable]
public class Wave
{
    public TroopInfo[] troops;
}
public class EnemyWaveManager : MonoBehaviour
{
    public Wave[] waves;
    public int totalWaves = 20;

    public void StartEnemyWave()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int waveIndex = 0; waveIndex < totalWaves; waveIndex++)
        {
            Wave currentWave = waves[Mathf.Min(waveIndex, waves.Length - 1)];
            Debug.Log("Wave " + (waveIndex + 1) + ":");

            foreach (TroopInfo troopInfo in currentWave.troops)
            {
                Debug.Log(troopInfo.troopCount + " " + troopInfo.troopPrefab.name + "(s)");

                for (int i = 0; i < troopInfo.troopCount; i++)
                {
                    Instantiate(troopInfo.troopPrefab);
                    float randomDelayPerSpawn = Random.Range(0.3f, 0.6f);
                    // Adjust the delay between troop spawns if needed
                    yield return new WaitForSeconds(randomDelayPerSpawn);
                }
            }

            // Adjust the delay between waves if needed
            yield return new WaitForSeconds(20f);
        }
    }
}
