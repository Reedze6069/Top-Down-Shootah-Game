using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBetweenSpawns = 1f;
    float nextSpawnTime;

    public EnemyData normalEnemy;
    public EnemyData strongEnemy;
    public EnemyData eliteEnemy;

    public Transform[] spawnPoints;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            float gameTime = Time.timeSinceLevelLoad;
            EnemyData enemyToSpawn = normalEnemy;

            if (gameTime >= 60f && Random.value < 0.05f)
            {
                enemyToSpawn = eliteEnemy;
            }
            else if (gameTime >= 20f && Random.value < 0.2f)
            {
                enemyToSpawn = strongEnemy;
            }

            if (enemyToSpawn != null && enemyToSpawn.prefab != null)
            {
                GameObject clone = Instantiate(enemyToSpawn.prefab, spawnPoint.position, Quaternion.identity);

                Enemy enemy = clone.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.health = enemyToSpawn.health;
                    enemy.speed = enemyToSpawn.speed;
                    enemy.shouldRotate = enemyToSpawn.shouldRotate;
                    enemy.rotationSpeed = enemyToSpawn.rotationSpeed;
                    enemy.deathEffectPrefab = enemyToSpawn.deathEffect;
                }
                else
                {
                    Debug.LogWarning("Spawned prefab is missing Enemy script!");
                }
            }
            else
            {
                Debug.LogWarning("EnemyData or its prefab is null.");
            }
        }
    }

    // ✅ Purple wireframe gizmos for spawn points
    void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        Gizmos.color = new Color(0.6f, 0f, 1f, 1f); // Purple

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null) continue;

            Gizmos.DrawWireSphere(spawnPoints[i].position, 0.5f);

#if UNITY_EDITOR
            UnityEditor.Handles.Label(spawnPoints[i].position + Vector3.up * 0.5f, $"SpawnPoint {i}");
#endif
        }
    }
}
