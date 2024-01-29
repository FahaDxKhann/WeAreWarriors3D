using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyWaveManager : MonoBehaviour
{
    public int totalWaveCount;
    private int currentWave;

    public List<int> enemysPerWave = new List<int>();



    public void StartEnemyWave()
    {
        InvokeRepeating("StartInvoke", 2, 25);
    }

    public void StartInvoke()
    {
        StartCoroutine(StartGenerating(0));
    }

    public IEnumerator StartGenerating(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < enemysPerWave[currentWave]; i++)
        {
            Controller.self.troopsManager.enemyHouse.GetComponent<Animation>().Play();
            Instantiate(Controller.self.troopsManager.enemyTroops[0]);
            yield return new WaitForSeconds(0.5f);
        }

        currentWave++;
    }
}
