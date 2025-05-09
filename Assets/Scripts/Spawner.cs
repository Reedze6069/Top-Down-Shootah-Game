using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBetweenSpawns;
    float nextSpawnTime;

    public GameObject normalEnemy;
    public GameObject strongEnemy;
    public GameObject eliteEnemy;

    public Transform[] spawnPoints;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            float gameTime = Time.timeSinceLevelLoad;
            GameObject enemyToSpawn = normalEnemy;

            if (gameTime >= 60f)
            {
                //5% chance to spawn elite enemy
                float eliteChance = Random.Range(0f, 1f);
                if (eliteChance < 0.05f)
                {
                    enemyToSpawn = eliteEnemy;
                }
                else
                {
                    // Otherwise, 20% chance for strong
                    float strongChance = Random.Range(0f, 1f);
                    if (strongChance < 0.2f)
                        enemyToSpawn = strongEnemy;
                }
            }
            else if (gameTime >= 30f)
            {
                // 20% chance to spawn strong enemy
                float strongChance = Random.Range(0f, 1f);
                if (strongChance < 0.2f)
                    enemyToSpawn = strongEnemy;
            }

            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}