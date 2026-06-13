using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnpoint[] enemySpawnpoints;
    public int timeBetweenWaves = 60;

    [SerializeField] private EnemyWave[] enemyWaves;
    [SerializeField] private TMPro.TextMeshProUGUI wavesText;
    [SerializeField] private TMPro.TextMeshProUGUI waveCountdownText;
    [SerializeField] private GameObject waveSpawnButton;

    int wave = 0;
    float timer = 0;

    private List<int> tunnelsUsed;

    int spawnedEnemies = 0;
    bool waveActive = false;

    void Start()
    {
        wave = 0;
        LoadNextWave();
    }

    public void LoadNextWave()
    {
        wave++;
        EnemyWave currentWave = enemyWaves[wave-1];

        tunnelsUsed = new List<int>();

        int differentDirections = currentWave.directions;

        List<int> availableTunnels = new List<int>();
        availableTunnels.Add(0);
        availableTunnels.Add(1);
        availableTunnels.Add(2);
        availableTunnels.Add(3);

        for(int i = 0; i < differentDirections; i++)
        {
            int rnd = Random.Range(0, availableTunnels.Count);
            tunnelsUsed.Add(availableTunnels[rnd]);
            availableTunnels.RemoveAt(rnd);
        }

        List<GameObject> enemiesToSpawn = new List<GameObject>();

        for(int j = 0; j < currentWave.enemies.Length; j++)
        {
            for(int k = 0; k < currentWave.enemyGroupSize[j]; k++)
            {
                enemiesToSpawn.Add(currentWave.enemies[j]);
            }
        }

        for(int i = 0; i < tunnelsUsed.Count; i++)
        {
            enemySpawnpoints[tunnelsUsed[i]].SetWave(enemiesToSpawn);
        }

        wavesText.text = "Wave: " + wave + "/" + enemyWaves.Length;
    }

    public void SpawnWave()
    {
        for(int i = 0; i < tunnelsUsed.Count; i++)
        {
            enemySpawnpoints[tunnelsUsed[i]].SpawnWave();
        }
    }

    public void SpawnWaveFaster()
    {
        FindAnyObjectByType<Resources>().ChangeMoney(Mathf.RoundToInt(timeBetweenWaves - timer)*10);
        OnCountDownEnd();
    }

    public void ChangeSpawnedEnemies(int value)
    {
        spawnedEnemies += value;

        if(spawnedEnemies <= 0)
        {
            if(wave >= enemyWaves.Length)
            {
                FindAnyObjectByType<GameManage>().Win();
            }
            else
            {
                LoadNextWave();
                waveActive = false;
                waveCountdownText.gameObject.SetActive(true);
                waveSpawnButton.SetActive(true);
            }
        }
    }

    void Update()
    {
        if(!waveActive)
        {
            timer += Time.deltaTime;
            waveCountdownText.text = "Next wave in: " + Mathf.RoundToInt(timeBetweenWaves - timer);

            if(timer >= timeBetweenWaves)
            {
                OnCountDownEnd();
            }
        }
    }
    
    void OnCountDownEnd()
    {
        timer = 0;
        SpawnWave();
        waveActive = true;
        waveCountdownText.gameObject.SetActive(false);
        waveSpawnButton.SetActive(false);
    }
}
