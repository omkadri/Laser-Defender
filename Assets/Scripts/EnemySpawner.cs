using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs; //allows us to attach the wave scriptable objects
    int StartingWave = 0;
    

    void Start()
    {
        var currentWave = waveConfigs[StartingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); //the wave config from this function is being passed into the SetWaveConfig function in the EnemyPathing script
            yield return new WaitForSeconds(waveConfig.GettimeBetweenSpawns());
        }
    }
}
