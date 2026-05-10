using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{

    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;

    [SerializeField] private float spawnInterval = 3;

    [SerializeField] private GameObject activeSpawnMark;

    List<GameObject> enemies;

    float timer = 0;

    bool spawning = false;

    int enemyCount;

    void Update()
    {
        if(spawning)
        {
            timer += Time.deltaTime;

            if(timer >= spawnInterval)
            {
                timer = 0;
                SpawnPartOfTheWave();
            }
        }
    }

    public void SetWave(List<GameObject> newEnemies)
    {
        enemies = new List<GameObject>();

        for(int i = 0; i < newEnemies.Count; i++)
        {
            enemies.Add(newEnemies[i]);
        }

        activeSpawnMark.SetActive(true);
    }

    public void SpawnWave()
    {
        enemyCount = 0;
        spawning = true;
        timer = 0;
        SpawnPartOfTheWave();
    }

    public void SpawnPartOfTheWave()
    {
        SpawnEnemy(enemies[enemyCount], spawn1);
        SpawnEnemy(enemies[enemyCount], spawn2);

        enemyCount += 2;
        FindAnyObjectByType<EnemySpawner>().ChangeSpawnedEnemies(2);

        if(enemyCount >= enemies.Count)
        {
            spawning = false;
            activeSpawnMark.SetActive(false);   
        }
    }

    public void SpawnEnemy(GameObject enemy, Transform enemySpawnpoint)
    {
        GameObject newEnemy = Instantiate(enemy, enemySpawnpoint.position, enemySpawnpoint.rotation);
    }
    
}
